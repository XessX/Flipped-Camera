/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam.Internal {

    using AOT;
    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class CameraDeviceiOS : CameraDevice {

        #region --Introspection--

        public new static CameraDeviceiOS[] GetDevices () {
            // Get native devices
            IntPtr deviceArray;
            int deviceCount;
            CameraDeviceBridge.GetDevices(out deviceArray, out deviceCount);
            // Check permissions
            if (deviceArray == IntPtr.Zero)
                return null;
            // Marshal
            var devices = new CameraDeviceiOS[deviceCount];
            for (var i = 0; i < deviceCount; i++) {
                var device = Marshal.ReadIntPtr(deviceArray, i * Marshal.SizeOf(typeof(IntPtr)));
                devices[i] = new CameraDeviceiOS(device);
            }
            Marshal.FreeCoTaskMem(deviceArray);
            return devices;
        }
        #endregion


        #region --Properties--
        
        public override string UniqueID {
            get {
                var result = new StringBuilder(1024);
                device.DeviceUID(result);
                return result.ToString();
            }
        }

        public override bool IsFrontFacing => device.IsFrontFacing();

        public override bool IsFlashSupported => device.IsFlashSupported();

        public override bool IsTorchSupported => device.IsTorchSupported();

        public override bool IsExposureLockSupported => device.IsExposureLockSupported();

        public override bool IsFocusLockSupported => device.IsFocusLockSupported();

        public override bool IsWhiteBalanceLockSupported => device.IsWhiteBalanceLockSupported();

        public override float HorizontalFOV => device.HorizontalFOV();

        public override float VerticalFOV => device.VerticalFOV();

        public override float MinExposureBias => device.MinExposureBias();

        public override float MaxExposureBias => device.MaxExposureBias();

        public override float MaxZoomRatio => device.MaxZoomRatio();
        #endregion


        #region --Settings--

        public override (int width, int height) PreviewResolution {
            get {
                device.GetPreviewResolution(out var width, out var height);
                return (width, height);
            }
            set => device.SetPreviewResolution(value.width, value.height);
        }

        public override (int width, int height) PhotoResolution {
            get {
                device.GetPhotoResolution(out var width, out var height);
                return (width, height);
            }
            set => device.SetPhotoResolution(value.width, value.height);
        }
        
        public override int Framerate {
            get => device.GetFramerate();
            set => device.SetFramerate(value);
        }

        public override float ExposureBias {
            get => device.GetExposureBias();
            set => device.SetExposureBias(value);
        }

        public override bool ExposureLock {
            get => device.GetExposureLock();
            set => device.SetExposureLock(value);
        }

        public override (float x, float y) ExposurePoint {
            set => device.SetExposurePoint(value.x, value.y);
        }

        public override FlashMode FlashMode {
            get => device.GetFlashMode();
            set => device.SetFlashMode(value);
        }

        public override bool FocusLock {
            get => device.GetFocusLock();
            set => device.SetFocusLock(value);
        }

        public override (float x, float y) FocusPoint {
            set => device.SetFocusPoint(value.x, value.y);
        }

        public override bool TorchEnabled {
            get => device.GetTorchEnabled();
            set => device.SetTorchEnabled(value);
        }

        public override bool WhiteBalanceLock {
            get => device.GetWhiteBalanceLock();
            set => device.SetWhiteBalanceLock(value);
        }

        public override float ZoomRatio {
            get => device.GetZoomRatio();
            set => device.SetZoomRatio(value);
        }
        #endregion


        #region --DeviceCamera--

        public override bool IsRunning => device.IsRunning();

        public override void StartPreview (Action<Texture2D> startCallback, Action<long> frameCallback, ScreenOrientation rotation) {
            rotation = rotation != 0 ? rotation : Screen.orientation;
            Action<IntPtr, int, int, long> handler = (pixelBuffer, width, height, timestamp) => {
                // Update preview texture
                var firstFrame = !previewTexture;
                previewTexture = previewTexture ?? new Texture2D(width, height, TextureFormat.RGBA32, false, false);
                previewTexture.LoadRawTextureData(pixelBuffer, width * height * 4);
                previewTexture.Apply();
                // Send to handlers
                if (firstFrame)
                    startCallback(previewTexture);
                frameCallback?.Invoke(timestamp);
            };
            device.StartPreview((int)rotation, FrameDelegate, (IntPtr)GCHandle.Alloc(handler, GCHandleType.Normal));
        }

        public override void StopPreview () {
            device.StopPreview();
            Texture2D.Destroy(previewTexture);
            previewTexture = null;
        }

        public override void CapturePhoto (Action<Texture2D> callback) {
            GCHandle handle;
            Action<IntPtr, int, int, long> handler = (pixelBuffer, width, height, timestamp) => {
                var photo = new Texture2D(width, height, TextureFormat.BGRA32, false);
                photo.LoadRawTextureData(pixelBuffer, width * height * 4);
                photo.Apply();
                callback(photo);
                handle.Free();
            };
            handle = GCHandle.Alloc(handler, GCHandleType.Normal);
            device.CapturePhoto(FrameDelegate, (IntPtr)handle);
        }
        #endregion


        #region --Operations--

        private readonly IntPtr device;
        private Texture2D previewTexture;

        private CameraDeviceiOS (IntPtr device) => this.device = device;

        ~CameraDeviceiOS () => device.FreeDevice();

        [MonoPInvokeCallback(typeof(CameraDeviceBridge.FrameDelegate))]
        private static void FrameDelegate (IntPtr context, IntPtr pixelBuffer, int width, int height, long timestamp) => (((GCHandle)context).Target as Action<IntPtr, int, int, long>)(pixelBuffer, width, height, timestamp);
        #endregion
    }
}