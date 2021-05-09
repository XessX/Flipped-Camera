/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam.Internal {

    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class CameraDeviceBridge {

        private const string Assembly = @"__Internal";

        public delegate void FrameDelegate (IntPtr context, IntPtr pixelBuffer, int width, int height, long timestamp);

        #if UNITY_IOS && !UNITY_EDITOR

        [DllImport(Assembly, EntryPoint = @"NCGetDevices")]
        public static extern void GetDevices (out IntPtr outDevicesArray, out int outDevicesArrayCount);
        [DllImport(Assembly, EntryPoint = @"NCFreeDevice")]
        public static extern void FreeDevice (this IntPtr device);
        [DllImport(Assembly, EntryPoint = @"NCUniqueID")]
        public static extern void DeviceUID (this IntPtr device, StringBuilder dest);
        [DllImport(Assembly, EntryPoint = @"NCIsFrontFacing")]
        public static extern bool IsFrontFacing (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCIsFlashSupported")]
        public static extern bool IsFlashSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCIsTorchSupported")]
        public static extern bool IsTorchSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCIsExposureLockSupported")]
        public static extern bool IsExposureLockSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCIsFocusLockSupported")]
        public static extern bool IsFocusLockSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCIsWhiteBalanceLockSupported")]
        public static extern bool IsWhiteBalanceLockSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCHorizontalFOV")]
        public static extern float HorizontalFOV (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCVerticalFOV")]
        public static extern float VerticalFOV (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCMinExposureBias")]
        public static extern float MinExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCMaxExposureBias")]
        public static extern float MaxExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCMaxZoomRatio")]
        public static extern float MaxZoomRatio (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCGetPreviewResolution")]
        public static extern void GetPreviewResolution (this IntPtr camera, out int width, out int height);
        [DllImport(Assembly, EntryPoint = @"NCSetPreviewResolution")]
        public static extern void SetPreviewResolution (this IntPtr camera, int width, int height);
        [DllImport(Assembly, EntryPoint = @"NCGetPhotoResolution")]
        public static extern void GetPhotoResolution (this IntPtr camera, out int width, out int height);
        [DllImport(Assembly, EntryPoint = @"NCSetPhotoResolution")]
        public static extern void SetPhotoResolution (this IntPtr camera, int width, int height);
        [DllImport(Assembly, EntryPoint = @"NCGetFramerate")]
        public static extern int GetFramerate (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetFramerate")]
        public static extern void SetFramerate (this IntPtr camera, int framerate);
        [DllImport(Assembly, EntryPoint = @"NCGetExposureBias")]
        public static extern float GetExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetExposureBias")]
        public static extern void SetExposureBias (this IntPtr camera, float bias);
        [DllImport(Assembly, EntryPoint = @"NCSetExposurePoint")]
        public static extern void SetExposurePoint (this IntPtr camera, float x, float y);
        [DllImport(Assembly, EntryPoint = @"NCGetExposureLock")]
        public static extern bool GetExposureLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetExposureLock")]
        public static extern void SetExposureLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = @"NCGetFlashMode")]
        public static extern FlashMode GetFlashMode (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetFlashMode")]
        public static extern void SetFlashMode (this IntPtr camera, FlashMode state);
        [DllImport(Assembly, EntryPoint = @"NCGetFocusLock")]
        public static extern bool GetFocusLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetFocusLock")]
        public static extern void SetFocusLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = @"NCSetFocusPoint")]
        public static extern void SetFocusPoint (this IntPtr camera, float x, float y);
        [DllImport(Assembly, EntryPoint = @"NCGetTorchEnabled")]
        public static extern bool GetTorchEnabled (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetTorchEnabled")]
        public static extern void SetTorchEnabled (this IntPtr camera, bool enabled);
        [DllImport(Assembly, EntryPoint = @"NCGetWhiteBalanceLock")]
        public static extern bool GetWhiteBalanceLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetWhiteBalanceLock")]
        public static extern void SetWhiteBalanceLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = @"NCGetZoomRatio")]
        public static extern float GetZoomRatio (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCSetZoomRatio")]
        public static extern void SetZoomRatio (this IntPtr camera, float ratio);
        [DllImport(Assembly, EntryPoint = @"NCIsRunning")]
        public static extern bool IsRunning (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCStartPreview")]
        public static extern void StartPreview (this IntPtr camera, int rotation, FrameDelegate frameHandler, IntPtr context);
        [DllImport(Assembly, EntryPoint = @"NCStopPreview")]
        public static extern void StopPreview (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = @"NCCapturePhoto")]
        public static extern void CapturePhoto (this IntPtr camera, FrameDelegate handler, IntPtr context);
        
        #else
        public static void GetDevices (out IntPtr outDevicesArray, out int outDevicesArrayCount) { outDevicesArray = IntPtr.Zero; outDevicesArrayCount = 0; }
        public static void FreeDevice (this IntPtr device) {}
        public static void DeviceUID (this IntPtr device, StringBuilder dest) {}
        public static bool IsFrontFacing (this IntPtr camera) => false;
        public static bool IsFlashSupported (this IntPtr camera) => false;
        public static bool IsTorchSupported (this IntPtr camera) => false;
        public static bool IsExposureLockSupported (this IntPtr camera) => false;
        public static bool IsFocusLockSupported (this IntPtr camera) => false;
        public static bool IsWhiteBalanceLockSupported (this IntPtr camera) => false;
        public static float HorizontalFOV (this IntPtr camera) => 0;
        public static float VerticalFOV (this IntPtr camera) => 0;
        public static float MinExposureBias (this IntPtr camera) => 0;
        public static float MaxExposureBias (this IntPtr camera) => 0;
        public static float MaxZoomRatio (this IntPtr camera) => 1;
        public static void GetPreviewResolution (this IntPtr camera, out int width, out int height) { width = height = 0; }
        public static void SetPreviewResolution (this IntPtr camera, int width, int height) {}
        public static void GetPhotoResolution (this IntPtr camera, out int width, out int height) { width = height = 0; }
        public static void SetPhotoResolution (this IntPtr camera, int width, int height) {}
        public static int GetFramerate (this IntPtr camera) => 0;
        public static void SetFramerate (this IntPtr camera, int framerate) {}
        public static float GetExposureBias (this IntPtr camera) => 0;
        public static void SetExposureBias (this IntPtr camera, float bias) {}
        public static void SetExposurePoint (this IntPtr camera, float x, float y) {}
        public static bool GetExposureLock (this IntPtr camera) => false;
        public static void SetExposureLock (this IntPtr camera, bool locked) {}
        public static FlashMode GetFlashMode (this IntPtr camera) => 0;
        public static void SetFlashMode (this IntPtr camera, FlashMode state) {}
        public static bool GetFocusLock (this IntPtr camera) => false;
        public static void SetFocusLock (this IntPtr camera, bool locked) {}
        public static void SetFocusPoint (this IntPtr camera, float x, float y) {}
        public static bool GetTorchEnabled (this IntPtr camera) => false;
        public static void SetTorchEnabled (this IntPtr camera, bool state) {}
        public static bool GetWhiteBalanceLock (this IntPtr camera) => false;
        public static void SetWhiteBalanceLock (this IntPtr camera, bool locked) {}
        public static float GetZoomRatio (this IntPtr camera) => 0;
        public static void SetZoomRatio (this IntPtr camera, float ratio) {}
        public static bool IsRunning (this IntPtr camera) => false;
        public static void StartPreview (this IntPtr camera, int rotation, FrameDelegate frameHandler, IntPtr context) {}
        public static void StopPreview (this IntPtr camera) {}
        public static void CapturePhoto (this IntPtr camera, FrameDelegate photoHandler, IntPtr context) {}
        #endif
    }
}