/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam.Internal {

    using UnityEngine;
    using UnityEngine.Scripting;
    using System;

    public sealed class CameraDeviceAndroid : CameraDevice {

        #region --Introspection--

        public new static CameraDeviceAndroid[] GetDevices () {
            try {
                using (var CameraDevice = new AndroidJavaClass(@"api.natsuite.natcam.CameraDevice"))
                    using (var devicesArray = CameraDevice.CallStatic<AndroidJavaObject>(@"getDevices")) {
                        var devices = AndroidJNIHelper.ConvertFromJNIArray<AndroidJavaObject[]>(devicesArray.GetRawObject());
                        var result = new CameraDeviceAndroid[devices.Length];
                        for (var i = 0; i < devices.Length; i++)
                            result[i] = new CameraDeviceAndroid(devices[i]);
                        return result;
                    }
            } catch { return null; } // Permissions denied
        }
        #endregion


        #region --Properties--

        public override string UniqueID => device.Call<string>(@"uniqueID");

        public override bool IsFrontFacing => device.Call<bool>(@"isFrontFacing");

        public override bool IsFlashSupported => device.Call<bool>(@"isFlashSupported");

        public override bool IsTorchSupported => device.Call<bool>(@"isTorchSupported");

        public override bool IsExposureLockSupported => device.Call<bool>(@"isExposureLockSupported");

        public override bool IsFocusLockSupported => device.Call<bool>(@"isFocusLockSupported");

        public override bool IsWhiteBalanceLockSupported => device.Call<bool>(@"isWhiteBalanceLockSupported");

        public override float HorizontalFOV => device.Call<float>(@"horizontalFOV");
        
        public override float VerticalFOV => device.Call<float>(@"verticalFOV");

        public override float MinExposureBias => device.Call<float>(@"minExposureBias");

        public override float MaxExposureBias => device.Call<float>(@"maxExposureBias");

        public override float MaxZoomRatio => device.Call<float>(@"maxZoomRatio");
        #endregion


        #region --Settings--

        public override (int width, int height) PreviewResolution {
            get {
                using (var size = device.Call<AndroidJavaObject>(@"getPreviewResolution"))
                    return (size.Call<int>(@"getWidth"), size.Call<int>(@"getHeight"));
            }
            set => device.Call(@"setPreviewResolution", value.width, value.height);
        }

        public override (int width, int height) PhotoResolution {
            get {
                using (var size = device.Call<AndroidJavaObject>(@"getPhotoResolution"))
                    return (size.Call<int>(@"getWidth"), size.Call<int>(@"getHeight"));
            }
            set => device.Call(@"setPhotoResolution", value.width, value.height);
        }
        
        public override int Framerate {
            get => device.Call<int>(@"getFramerate");
            set => device.Call(@"setFramerate", value);
        }

        public override float ExposureBias {
            get => device.Call<float>(@"getExposureBias");
            set => device.Call(@"setExposureBias", (int)value);
        }

        public override bool ExposureLock {
            get => device.Call<bool>(@"getExposureLock");
            set => device.Call(@"setExposureLock", value);
        }

        public override (float x, float y) ExposurePoint {
            set => device.Call(@"setExposurePoint", value.x, value.y);
        }

        public override FlashMode FlashMode {
            get => (FlashMode)device.Call<int>(@"getFlashMode");
            set => device.Call(@"setFlashMode", (int)value);
        }

        public override bool FocusLock {
            get => device.Call<bool>(@"getFocusLock");
            set => device.Call(@"setFocusLock", value);
        }

        public override (float x, float y) FocusPoint {
            set => device.Call(@"setFocusPoint", value.x, value.y);
        }

        public override bool TorchEnabled {
            get => device.Call<bool>(@"getTorchEnabled");
            set => device.Call(@"setTorchEnabled", value);
        }

        public override bool WhiteBalanceLock {
            get => device.Call<bool>(@"getWhiteBalanceLock");
            set => device.Call(@"setWhiteBalanceLock", value);
        }

        public override float ZoomRatio {
            get => device.Call<float>(@"getZoomRatio");
            set => device.Call(@"setZoomRatio", value);
        }
        #endregion


        #region --DeviceCamera--

        public override bool IsRunning => device.Call<bool>(@"isRunning");

        public override void StartPreview (Action<Texture2D> startCallback, Action<long> frameCallback, ScreenOrientation rotation) {
            rotation = rotation != 0 ? rotation : Screen.orientation;
            device.Call(@"startPreview", (int)rotation, new FrameDelegate((pixelBuffer, width, height, timestamp) => {
                // Update preview texture
                var firstFrame = !previewTexture;
                previewTexture = previewTexture ?? new Texture2D(width, height, TextureFormat.RGBA32, false, false);
                previewTexture.LoadRawTextureData(pixelBuffer, width * height * 4);
                previewTexture.Apply();
                // Send to handlers
                if (firstFrame)
                    startCallback(previewTexture);
                frameCallback?.Invoke(timestamp);
            }));
        }

        public override void StopPreview () {
            device.Call(@"stopPreview");
            Texture2D.Destroy(previewTexture);
            previewTexture = null;
        }

        public override void CapturePhoto (Action<Texture2D> callback) => device.Call(@"capturePhoto", new FrameDelegate((pixelBuffer, width, height, timestamp) => {
            var photoTexture = new Texture2D(width, height, TextureFormat.RGBA32, false, false);
            photoTexture.LoadRawTextureData(pixelBuffer, width * height * 4);
            photoTexture.Apply();
            callback(photoTexture);
        }));
        #endregion


        #region --Operations--

        private readonly AndroidJavaObject device;
        private Texture2D previewTexture;

        private CameraDeviceAndroid (AndroidJavaObject device) => this.device = device;

        ~CameraDeviceAndroid () =>  device.Dispose();

        private sealed class FrameDelegate : AndroidJavaProxy {
            private readonly Action<IntPtr, int, int, long> handler;
            public FrameDelegate (Action<IntPtr, int, int, long> handler) : base(@"api.natsuite.natcam.CameraDevice$FrameDelegate") => this.handler = handler;
            [Preserve] private void onFrame (long pixelBuffer, int width, int height, long timestamp) => handler((IntPtr)pixelBuffer, width, height, timestamp);
        }
        #endregion
    }
}