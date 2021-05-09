/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam {

    using UnityEngine;
    using System;
    using Internal;

    /// <summary>
    /// This class provides an abstraction for hardware cameras
    /// </summary>
    [Doc(@"CameraDevice")]
    public abstract class CameraDevice {

        #region ---Introspection---
        /// <summary>
        /// Get all camera devices.
        /// If permissions have not been granted, this method will return `null`.
        /// </summary>
        [Doc(@"GetCameras"), Code(@"EnableTorch")]
        public static CameraDevice[] GetDevices () {
            switch (Application.platform) {
                case RuntimePlatform.Android: return CameraDeviceAndroid.GetDevices();
                case RuntimePlatform.IPhonePlayer: return CameraDeviceiOS.GetDevices();
                default: return CameraDeviceLegacy.GetDevices();
            }
        }
        #endregion


        #region --Properties--
        /// <summary>
        /// Device unique ID
        /// </summary>
        [Doc(@"UniqueID")]
        public abstract string UniqueID { get; }
        /// <summary>
        /// Is the camera front facing?
        /// </summary>
        [Doc(@"IsFrontFacing")]
        public abstract bool IsFrontFacing { get; }
        /// <summary>
        /// Does this camera support flash?
        /// </summary>
        [Doc(@"IsFlashSupported")]
        public abstract bool IsFlashSupported { get; }
        /// <summary>
        /// Does this camera support torch?
        /// </summary>
        [Doc(@"IsTorchSupported")]
        public abstract bool IsTorchSupported { get; }
        /// <summary>
        /// Does this camera support exposure lock?
        /// </summary>
        [Doc(@"IsExposureLockSupported")]
        public abstract bool IsExposureLockSupported { get; }
        /// <summary>
        /// Does this camera support focus lock?
        /// </summary>
        [Doc(@"IsFocusLockSupported")]
        public abstract bool IsFocusLockSupported { get; }
        /// <summary>
        /// Does this camera support white balance lock?
        /// </summary>
        [Doc(@"IsWhiteBalanceLockSupported")]
        public abstract bool IsWhiteBalanceLockSupported { get; }
        /// <summary>
        /// Get the camera's horizontal field-of-view
        /// </summary>
        [Doc(@"HorizontalFOV")]
        public abstract float HorizontalFOV { get; }
        /// <summary>
        /// Get the camera's vertical field-of-view
        /// </summary>
        [Doc(@"VerticalFOV")]
        public abstract float VerticalFOV { get; }
        /// <summary>
        /// Get the camera's minimum exposure bias
        /// </summary>
        [Doc(@"MinExposureBias")]
        public abstract float MinExposureBias { get; }
        /// <summary>
        /// Get the camera's maximum exposure bias
        /// </summary>
        [Doc(@"MaxExposureBias")]
        public abstract float MaxExposureBias { get; }
        /// <summary>
        /// Get the camera's maximum zoom ratio
        /// </summary>
        [Doc(@"MaxZoomRatio")]
        public abstract float MaxZoomRatio { get; }
        #endregion


        #region ---Settings---
        /// <summary>
        /// Get or set the current preview resolution of the camera
        /// </summary>
        [Doc(@"PreviewResolution")]
        public abstract (int width, int height) PreviewResolution { get; set; }
        /// <summary>
        /// Get or set the current photo resolution of the camera
        /// </summary>
        [Doc(@"PhotoResolution")]
        public abstract (int width, int height) PhotoResolution { get; set; }
        /// <summary>
        /// Get or set the current framerate of the camera
        /// </summary>
        [Doc(@"Framerate")]
        public abstract int Framerate { get; set; }
        /// <summary>
        /// Get or set the camera's exposure lock
        /// </summary>
        [Doc(@"ExposureLock")]
        public abstract bool ExposureLock { get; set; }
        /// <summary>
        /// Get or set the camera's exposure bias
        /// </summary>
        [Doc(@"ExposureBias", @"ExposureBiasDiscussion")]
        public abstract float ExposureBias { get; set; }
        /// <summary>
        /// Set the camera's exposure point of interest
        /// </summary>
        //[Doc(@"ExposurePoint", @"ExposurePointDiscussion"), Code(@"ExposeCamera")]
        public abstract (float x, float y) ExposurePoint { set; }
        /// <summary>
        /// Get or set the camera's flash mode when taking a picture
        /// </summary>
        [Doc(@"CameraFlashMode")]
        public abstract FlashMode FlashMode { get; set; }
        /// <summary>
        /// Get or set the camera's focus lock
        /// </summary>
        [Doc(@"FocusLock")]
        public abstract bool FocusLock { get; set; }
        /// <summary>
        /// Set the camera's focus point of interest
        /// </summary>
        //[Doc(@"FocusPoint", @"FocusPointDiscussion"), Code(@"FocusCamera")]
        public abstract (float x, float y) FocusPoint { set; }
        /// <summary>
        /// Get or set the camera's torch mode
        /// </summary>
        [Doc(@"TorchEnabled")]
        public abstract bool TorchEnabled { get; set; }
        /// <summary>
        /// Get or set the camera's white balance lock
        /// </summary>
        [Doc(@"WhiteBalanceLock")]
        public abstract bool WhiteBalanceLock { get; set; }
        /// <summary>
        /// Get or set the camera's current zoom ratio. This value must be between [1, MaxZoomRatio]
        /// </summary>
        [Doc(@"ZoomRatio")]
        public abstract float ZoomRatio { get; set; }
        #endregion


        #region --Operations--
        /// <summary>
        /// Is the camera running?
        /// </summary>
        [Doc(@"IsRunning")]
        public abstract bool IsRunning { get; }

        /// <summary>
        /// Start the camera preview
        /// </summary>
        /// <param name="startCallback">Callback invoked when the preview starts</param>
        /// <param name="frameCallback">Optional. Callback invoked when a new preview frame is available with corresponding timestamp in nanoseconds.</param>
        /// <param name="rotation"></param>
        [Doc(@"StartPreview")]
        public abstract void StartPreview (Action<Texture2D> startCallback, Action<long> frameCallback = null, ScreenOrientation rotation = 0);

        /// <summary>
        /// Stop the camera preview
        /// </summary>
        [Doc(@"StopPreview")]
        public abstract void StopPreview ();

        /// <summary>
        /// Capture a photo
        /// </summary>
        /// <param name="callback">The callback to be invoked when NatCam receives the captured photo</param>
        [Doc(@"CapturePhoto", @"CapturePhotoDiscussion"), Code(@"TakeAPhoto")]
        public abstract void CapturePhoto (Action<Texture2D> callback);
        #endregion
    }


    #region --Surrogate Types--
    /// <summary>
    /// Camera flash mode
    /// </summary>
    [Doc(@"FlashMode")]
    public enum FlashMode {
        [Doc(@"FlashOff")] Off = 0,
        [Doc(@"FlashOn")] On = 1,
        [Doc(@"FlashAuto")] Auto = 2
    }
    #endregion
}