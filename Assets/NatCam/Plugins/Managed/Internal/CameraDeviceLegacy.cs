/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam.Internal {

    using UnityEngine;
    using System;
    using System.Collections;
    using Stopwatch = System.Diagnostics.Stopwatch;

    public sealed class CameraDeviceLegacy : CameraDevice {

        #region --Introspection--

        public new static CameraDeviceLegacy[] GetDevices () {
            var devices = WebCamTexture.devices;
            var result = new CameraDeviceLegacy[devices.Length];
            for (var i = 0; i < devices.Length; i++)
                result[i] = new CameraDeviceLegacy(devices[i]);
            return result;
        }
        #endregion


        #region --Properties--

        public override string UniqueID => device.name;

        public override bool IsFrontFacing => device.isFrontFacing;

        public override bool IsFlashSupported => false;

        public override bool IsTorchSupported => false;

        public override bool IsExposureLockSupported => false;

        public override bool IsFocusLockSupported => false;

        public override bool IsWhiteBalanceLockSupported => false;

        public override float HorizontalFOV {
            get {
                Debug.LogWarning("NatCam Error: Field of view is not supported on legacy backend");
                return 0f;
            }
        }

        public override float VerticalFOV {
            get {
                Debug.LogWarning("NatCam Error: Field of view is not supported on legacy backend");
                return 0f;
            }
        }

        public override float MinExposureBias => 0f;

        public override float MaxExposureBias => 0f;

        public override float MaxZoomRatio => 1f;
        #endregion


        #region --Settings--

        public override (int width, int height) PreviewResolution {
            get; set;
        }

        public override (int width, int height) PhotoResolution {
            get => PreviewResolution;
            set { }
        }

        public override int Framerate {
            get; set;
        }

        public override float ExposureBias {
            get => 0f;
            set { }
        }

        public override bool ExposureLock {
            get => false;
            set { }
        }

        public override (float x, float y) ExposurePoint {
            set { }
        }

        public override FlashMode FlashMode {
            get => 0;
            set { }
        }

        public override bool FocusLock {
            get => false;
            set { }
        }

        public override (float x, float y) FocusPoint {
            set { }
        }

        public override bool TorchEnabled {
            get => false;
            set { }
        }

        public override bool WhiteBalanceLock {
            get => false;
            set { }
        }

        public override float ZoomRatio {
            get => 1f;
            set { }
        }
        #endregion
        

        #region --DeviceCamera--

        public override bool IsRunning => webcamTexture;

        public override void StartPreview (Action<Texture2D> startCallback, Action<long> frameCallback, ScreenOrientation rotation) {
            this.startCallback = startCallback;
            this.frameCallback = frameCallback;
            webcamTexture = new WebCamTexture(device.name, PreviewResolution.width, PreviewResolution.height, Framerate);
            webcamTexture.Play();
            frameHelper = new GameObject("NatCam Helper").AddComponent<CameraDeviceAttachment>();
            frameHelper.StartCoroutine(Update());
        }

        public override void StopPreview () {
            CameraDeviceAttachment.Destroy(frameHelper);
            webcamTexture.Stop();
            WebCamTexture.Destroy(webcamTexture);
            Texture2D.Destroy(previewTexture);
            frameHelper = null;
            webcamTexture = null;
            previewTexture = null;
            startCallback = null;
            frameCallback = null;
            pixelBuffer = null;
        }

        public override void CapturePhoto (Action<Texture2D> callback) {
            var photo = new Texture2D(previewTexture.width, previewTexture.height, previewTexture.format, false, false);
            photo.SetPixels32(pixelBuffer);
            photo.Apply();
            callback(photo);
        }
        #endregion


        #region --Operations--

        private readonly WebCamDevice device;
        private Action<Texture2D> startCallback;
        private Action<long> frameCallback;
        private WebCamTexture webcamTexture;
        private Texture2D previewTexture;
        private Color32[] pixelBuffer;
        private CameraDeviceAttachment frameHelper;
        
        private CameraDeviceLegacy (WebCamDevice device) {
            this.device = device;
            this.PreviewResolution = (1280, 720);
            this.Framerate = 30;
        }

        private IEnumerator Update () {
            for (;;) {
                yield return new WaitForEndOfFrame();
                if (webcamTexture.width == 16 || webcamTexture.height == 16)
                    continue;
                // Update preview texture
                var firstFrame = !previewTexture;
                previewTexture = previewTexture ?? new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false, false);
                pixelBuffer = pixelBuffer ?? webcamTexture.GetPixels32();
                webcamTexture.GetPixels32(pixelBuffer);
                previewTexture.SetPixels32(pixelBuffer);
                previewTexture.Apply();
                // Invoke handlers
                if (firstFrame)
                    startCallback(previewTexture);
                frameCallback?.Invoke(Stopwatch.GetTimestamp() * 100L);
            }
        }

        private class CameraDeviceAttachment : MonoBehaviour { }
        #endregion
    }
}