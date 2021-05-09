## NatCam 2.3.1
+ Added Vulkan support on Android. Using Vulkan is strongly recommended over OpenGL ES as it offers better performance.
+ Added support for ultra-wide angle camera on iPhone 11.
+ Changed `CameraDevice.PreviewResolution` and `CameraDevice.PhotoResolution` types to tuples.
+ Changed `CameraDevice.ExposurePoint` and `CameraDevice.FocusPoint` types to tuples.
+ Fixed captured photo having lower resolution than was reported by `CameraDevice.PhotoResolution` on iOS.
+ Fixed flash not firing for photos on Android.
+ Fixed preview start callback being invoked multiple times on Android.
+ Fixed memory leak on Android.

## NatCam 2.3.0
+ Refactored `DeviceCamera` class to `CameraDevice` for parity with our other API's.
+ The preview texture provided by NatCam is now a `Texture2D`.
+ Removed support for autorotation due to technical constraints. Either lock your app's rotation when the camera is active or stop and restart the camera preview when the orientation changes.
+ Added `CameraDevice.IsExposureLockSupported` property.
+ Added `CameraDevice.IsFocusLockSupported` property.
+ Added `CameraDevice.IsWhiteBalanceLockSupported` property.
+ Changed `CameraDevice.PreviewResolution` and `CameraDevice.PhotoResolution` type from `Vector2Int` to `Resolution`.
+ Deprecated `CameraDevice.Cameras`, `CameraDevice.FrontCamera`, and `CameraDevice.RearCamera`. Use `CameraDevice.GetDevices` instead.
+ Deprecated `CameraDevice.CaptureFrame` method. Use `GetPixels32` or `GetRawTextureData` on the preview texture instead.
+ Fixed camera preview being dark on some Android devices.
+ Fixed framerate being low on some iOS devices.
+ Fixed focus lock being unset after `FocusPoint` was set on Android.
+ NatCam now requires a minimum of iOS 11 on iOS.
+ Reduced minimum requirement on Android to API level 21.

## NatCam 2.2.1
+ Accessing `DeviceCamera.Cameras` will automatically request permissions where necessary. If permissions are denied, the property will return `null`.
+ Fixed rare crash when `DeviceCamera.StopPreview` is called on Android.
+ Fixed the preview going black when the app is reoriented on Android.
+ Fixed `DeviceCamera.TorchMode` not working when the preview is running on Android.

## NatCam 2.2.0
+ Completely overhauled front-end API to provide a more device-oriented approach to handling device cameras. See the README for more info.
+ NatCam now supports running more than one device camera simultaneously on devices that support it.
+ Greatly improved camera preview framerate on lower-end Android devices. As a result, NatCam now requires API level 23 on Android.
+ Added the ability to set the exposure point. See `DeviceCamera.ExposurePoint`.
+ Fixed crash and glitching in camera preview when camera is switched on some Android devices.
+ Fixed bug where photo resolution was clamped to preview resolution on Android.
+ Fixed torch mode setting not working on some Android devices.
+ Changed `DeviceCamera.Framerate` type from `float` to `int`.
+ Refactored NatCam namespace from `NatCamU.Core` to `NatCam`.
+ Removed implicit cast between `DeviceCamera` and `int`.
+ Removed `INatCam.HasPermissions`. Use Unity's `Application.HasUserAuthorization` instead.
+ NatCam now requires Unity 2017.1 at least.

## NatCam 2.1.3
+ Added white balance lock. See `DeviceCamera.WhiteBalanceLock`.
+ Added focus lock. See `DeviceCamera.FocusLock`.
+ Added exposure lock. See `DeviceCamera.ExposureLock`.
+ Deprecated `DeviceCamera.AutofocusEnabled`.
+ Deprecated `DeviceCamera.AutoexposeEnabled`.
+ Fixed bitcode signature error when publishing to iTunes Store.

## NatCam 2.1f2
+ Dropped support for OpenGL ES on iOS. NatCam will only use Metal on iOS.
+ Fixed preview artifacts in some Android devices.
+ Fixed Play Store reporting 0 devices supported for Android apps built with NatCam.
+ Fixed zoom ratio setting not applying in captured photos on Android.

## NatCam 2.1f1
+ The Android backend has been updated to use the `camera2` API introduced in API level 22. As a result, performance has improved and we will be adding more `DeviceCamera` functions.
+ We have simplified the C# API. The `Play`, `Pause`, and `Release` functions have been replaced with clearer `StartPreview` and `StopPreview` functions.
+ We have simplified handling preview events and camera switching by changing `NatCam.StartPreview` to take in the camera and callbacks for the preview start and preview frame events.
+ Deprecated `NatCam.OnStart` and `NatCam.OnFrame` events.
+ Deprecated the `flip` flag in `NatCam.CaptureFrame`. Use OpenCV's `core.flip` function instead.
+ Deprecated `CameraResolution` struct, use `Vector2Int` instead.
+ Deprecated `DeviceCamera.FocusMode` property. Use `DeviceCamera.AutofocusEnabled` boolean instead.
+ Deprecated `FocusMode` enum.
+ Deprecated `DeviceCamera.ExposureMode` property. Use `DeviceCamera.AutoexposeEnabled` boolean instead.
+ Deprecated `ExposureMode` enum.
+ Deprecated `PreviewCallback` delegate type. Use `Action` instead.
+ Deprecated `PhotoCallback` delegate type. Use `Action<Texture2D>` instead.
+ Fixed crash on some Android devices when using `CaptureFrame` function.
+ Refactored `NatCam.IsPlaying` to `NatCam.IsRunning`.
+ Properly dispose of Java objects when on Android.
+ On Android, NatCam now requires at least API level 22 (Android Lollipop).

## NatCam 2.0f5
+ Fixed preview not restarting on Android.

## NatCam 2.0f4
+ Fixed null reference exception when `NatCam.Play` is called on Android.

## NatCam 2.0f3
+ Fixed preview being rotated wrongly in different device orientations.
+ Fixed `FlashMode.Auto` not working on iOS.

## NatCam 2.0f2
+ Greatly improved performance on Android.
+ Native sources will no longer be shared with developers. This is because we have made and will be making a lot of architectural changes, so it is not feasible for us to keep sharing the sources as we iterate.
+ `NatCam.CaptureFrame` is now `void`, because it will always succeed.
+ Fixed lag in camera preview when on some Galaxy S7 (Edge) devices. This lag required the `PreviewData` flag to be turned off. Now the lag has been resolved and the flag has been deprecated.
+ Fixed crash when `NatCam.Release` is called multiple times on Android.
+ Fixed front camera preview being upside down on Nexus 6P.

## NatCam 2.0f1
+ We have deprecated NatCam Core! From now on, NatCam Pro will be the standard NatCam spec.
+ We have deprecated video recording because we introduced a dedicated video recording API, [NatCorder](https://assetstore.unity.com/packages/tools/integration/natcorder-video-recording-api-102645).
+ We have renamed NatCam Pro to NatCam.
+ Refactored `NatCam.PreviewFrame` to `NatCam.CaptureFrame`.
+ Refactored `Resolution` struct to `CameraResolution` to avoid conflict with UnityEngine.
+ Added `flip` flag to `NatCam.CaptureFrame` for working with OpenCV where image must be vertically inverted.
+ Preview data format is now `RGBA32` on all platforms.
+ Deprecated `NatCam.PreviewBuffer` function. Use `NatCam.CaptureFrame` instead.
+ Deprecated `NatCam.PreviewMatrix` function. Use OpenCVForUnity's `Utils.texture2DtoMat` function instead.
+ Deprecated `NatCamBehaviour` helper class. Derive from `MonoBehaviour` and start NatCam manually.
+ Deprecated VisionCam example.
+ Deprecated `NatCam.Verbose` flag.
+ Removed `NATCAM_CORE` and `NATCAM_PRO` definitions.

## NatCam Core 1.6f2
+ All platforms will now return photos upright! This means that the `Orientation` enum, the `NatCamPanel` component, and the `Utilities::RotateImage` functions have been deprecated as they are not needed anymore.
+ Added `Resolution` struct. Use this for `DeviceCamera` resolution handling.
+ Changed `PhotoCallback` signature to only take a `Texture2D`, removed `Orientation`.
+ Fixed rare crash when `CapturePhoto` is called on Android.
+ Fixed preview resolution setting not taking effect on Legacy backend.
+ Deprecated `Facing` enum. Use `DeviceCamera.IsFrontFacing` instead.
+ Deprecated `ResolutionPreset` enum.
+ Deprecated `FrameratePreset` enum.
+ Deprecated `NatCamFocuser` component. It is now included as part of the MiniCam example.
+ Refactored `DeviceCamera.TorchMode` to `DeviceCamera.TorchEnabled`.
+ *Everything below*

## NatCam Pro 1.6f2
+ Made `NatCam.PreviewBuffer` thread-safe.
+ On Android, videos will now be recorded in the app's documents directory instead of in external storage.
+ Fixed crash when running NatCam Pro on Google Pixel 2.
+ *Everything below*

## NatCam Core 1.6f1
+ On Android, we have introduced an all new rendering pipeline. Features include:
    + Even faster NatCam preview rendering. NatCam now contributes **0ms** to your application's frame time
    + Multithreaded rendering. This makes NatCam much more energy efficient (phones don't get as hot now)
    + We have fixed the preview flickering issue encountered on some devices
+ On iOS and Legacy, all captured photos are now upright; the returned orientation is always `Rotation_0`.
+ Made IDispatch module independent of NatCam. You can now copy the IDispatch sources into any project.
+ Improved `NatCam.CapturePhoto` memory efficiency on Android and Legacy backend.
+ Captured photo texture format can now be different on different platforms.
+ Added `NatCamPanel` component for displaying photos upright on UI panels.
+ Redesigned MiniCam example UI.
+ Raised minimum Android API level to API level 18 (Android 4.3).
+ Added `TorchMode` enum.
+ Fixed crash caused by `NatCamBehaviour` when switching scenes.
+ Fixed bug on Android where camera resolution was changed after capturing photo.
+ Fixed bug on iOS where camera is not resumed when app is suspended and resumed.
+ Fixed bug on iOS where some photos become distorted.
+ Fixed exception when `CapturePhoto` is called on Android O.
+ Deprecated `NatCamPreview` component. For scaling, use UnityEngine.UI's `AspectRatioFitter`.
+ Deprecated `Switch` enum.
+ Deprecated `ZommMode` enum.
+ Deprecated `ScaleMode` enum.
+ Deprecated `FocusMode.MacroFocus`.
+ Deprecated `FocusMode.SoftFocus`.
+ Renamed native symbols to avoid clashes with other plugins.
+ Refactored `ResolutionPreset.HighestResolution` to `ResolutionPreset.Highest`.
+ Refactored `ResolutionPreset.MediumResolution` to `ResolutionPreset.Medium`.
+ Refactored `ResolutionPreset.LowestResolution` to `ResolutionPreset.Lowest`.
+ Changed `NatCam.Verbose` type to boolean.
+ *Everything below*

## NatCam Pro 1.6f1
+ NatCam Professional is now NatCam Pro.
+ Updated the ReplayCam example to be more modern.
+ NatCam Extended has been deprecated, including the SavePhoto API and metadata detection.
+ Added `Configuration` struct for setting video recording options.
+ Exposed keyframe interval for configuring video encoders.
+ Reduced `NatCamLegacy` memory footprint.
+ Fixed bug where `NatCam.PreviewBuffer`, `PreviewFrame`, and `PreviewMatrix` failed in the OnStart event.
+ Fixed iOS-recorded video appearing wrongly when viewed on Windows or Android.
+ Fixed rare crash on iOS when recording is stopped.
+ Fixed `NullPointerException` when `NatCam.StopRecording` is called on Android.
+ Deprecated `NatCamToMatHelper` component for OpenCV.
+ Deprecated `INatCam.SaveVideo(..)`.
+ Renamed native symbols to avoid clashes with other plugins.
+ Improved GreyCam example performance.
+ On iOS, microphone permission will only be requested if using video recording with audio.
+ Renamed `NATCAM_PROFESSIONAL` scripting definition symbol to `NATCAM_PRO`
+ *Everything below*

## NatCam Core 1.5f3
+ NatCam now supports autorotation.
+ Made preview rendering faster on Android.
+ Made PhotoCallback return orientation information.
+ NatCam.OnStart is now called when cameras are changed and when the orientation is changed.
+ Added a unified orientation pipeline for captured photos.
+ Added NatCamPreview component for properly displaying textures on UI panels with scaling and orientation.
+ Added NatCamFocuser component for detecting focus gestures.
+ Added Utilities.RotateImage(...) for rotating an image in system memory.
+ Added bitcode support on iOS.
+ Added support for zooming on a large amount of iOS and Android devices.
+ Added DeviceCamera.MaxZoomRatio.
+ Added tap-to-focus functionality to MiniCam example using NatCamFocuser.
+ Added DeviceCamera.Framerate getter.
+ Added Orientation struct for photo orientation.
+ Added NatCamTransform2D shader for GPU-accelerated image transformation.
+ Added ScaleMode.Letterbox for letterbox scaling.
+ Added NatCam.Implementation.HasPermissions boolean.
+ Refactored NatCam.OnPreviewStart, INatCam.OnPreviewStart, and NatCamBehaviour.OnPreviewStart to OnStart.
+ Refactored NatCam.OnPreviewUpdate, INatCam.OnPreviewUpdate, and NatCamBehaviour.OnPreviewUpdate to OnFrame.
+ Fixed bug where screen flickered when running on Android without multithreaded rendering.
+ Fixed bug on Android where calling Play() after Release() or pausing and suspending the app does not work.
+ Fixed bug on Android where camera does not pause when Pause() is called.
+ Fixed crash when NatCam.Release() is called on Android.
+ Fixed FocusMode.MacroFocus not working on Android.
+ Fixed DeviceCamera.FocusMode getter causing an error.
+ Fixed bug where DeviceCamera.SetFocus(..) ignored focus mode completely.
+ Fixed bug where OnStart is not called when preview is resumed on Android.
+ Fixed bug where setting high framerates rarely worked on iOS.
+ Fixed bug where preview stretched when using NatCamScaler and autorotation.
+ Fixed resource leak when NatCam.Release() is called after NatCam.Pause() on NatCamLegacy.
+ Fixed memory leak when taking photos with the MiniCam example.
+ Fixed potential namespace conflicts.
+ Fixed resolution set for one camera being used by another on NatCamLegacy.
+ Fixed IL2CPP build support on Android.
+ Fixed crash on Android when NatCam is unable to open the camera.
+ Fixed rare crash on Android when app lost focus with multithreaded rendering disabled.
+ Fixed exception on Android when NatCam.Camera getter is called before NatCam.Camera has been set.
+ Fixed DeviceCamera.SetFocus focus point being inverted on the Y axis.
+ Fixed bug on Android where switching cameras caused preview to freeze on older devices.
+ Fixed bug on Android where DeviceCamera.TorchMode getter always returned Off.
+ Fixed bug where preview was incorrectly oriented after NatCam.Release() was called.
+ Fixed bug on Android where preview would flip briefly when camera was switched.
+ Fixed bug on NatCamLegacy where preview did not update after Pause() was called.
+ Fixed bug on Android where setting a camera that did not exist caused a hard crash.
+ Fixed bug on iOS where suspending the app caused system sounds to be distorted.
+ Fixed bug on iOS where pausing the preview caused the orientation to be incorrect.
+ Fixed bug where IDispatch did not invoke any delegates when one of multiple cameras in the scene is disabled.
+ Fixed 'Camera is being used after Camera.release() was called' exception on Android.
+ Fixed bug on Android where z-sorting was affected by the preview.
+ Fixed build error when building Android project with NatCam and Android Ultimate Plugin.
+ Deprecated INatCamMobile.HasPermissions.
+ Deprecated INatCamMobile interface.
+ Deprecated NatCamTransformation2D shader. Use NatCamTransform2D instead.
+ Deprecated DeviceCamera.IsZoomSupported. Instead, check that DeviceCamera.MaxZoomRatio > 1.
+ Deprecated ZommMode enum.
+ Renamed native symbols to avoid clashes with other plugins.
+ Renamed Minigram example to MiniCam.
+ Renamed StartingOff example to PlainCam.
+ Refactored INatCamDispatch to IDispatch.
+ Refactored NatCamUtilities to Utilities.
+ Refactored ScaleMode.FixedHeightVariableWidth to ScaleMode.AdjustWidth.
+ Refactored ScaleMode.FixedWidthVariableHeight to ScaleMode.AdjustHeight.
+ Reimplemented NatCamPreview to scale the UI panel corners instead of vertices.
+ Made NatCamLegacy call OnPreviewStart only when Preview is not 16x16 (usually encountered on macOS).
+ IDispatch has been further modularized.
+ IDispatch will no more invoke a delegate more than once per Update.
+ Implemented IDisposable interface for IDispatch.
+ *Everything below*

## NatCam Pro 1.5f3
+ Added NatCam.PreviewMatrix(..) to greatly improve memory efficiency (so as not to allocate each time it is called).
+ Added NatCam.PreviewFrame(..) to greatly improve memory efficiency (so as not to allocate each time it is called).
+ Added NatCam.PreviewBuffer(..) overload that takes managed Color32[].
+ Added VisionCam example to demonstrate using NatCam with OpenCVForUnity.
+ Added ReadablePreview flag on NatCamAndroid. This is a workaround for the GPU driver bug that caused lag on the S7 Edge.
+ Added Configuration struct for setting video recording options.
+ Added bitcode support on iOS.
+ Added NatCamToMatHelper script for OpenCV/DLibFaceLandmarkDetector.
+ Exposed bitrate for configuring video encoders.
+ Exposed keyframe interval for configuring video encoders.
+ Reduced NatCamLegacy memory footprint.
+ Fixed crash when NatCam.StopRecording is called on Android.
+ Fixed crash when NatCam.StopRecording is called on iOS.
+ Fixed NatCam.PreviewMatrix having incorrect dimensions.
+ Fixed iOS-recorded video appearing wrongly when viewed on Windows or Android.
+ Fixed null reference exception when NatCam.PreviewBuffer is called on NatCamLegacy.
+ Fixed incorrect colors when using VisionCam example on iOS.
+ Deprecated NatCam.PreviewMatrix property.
+ Deprecated NatCam.PreviewFrame property.
+ Deprecated Utilities.SaveVideoToGallery. Use NatCam.Implementation.SaveVideo(string, SaveMode) instead.
+ Renamed native symbols to avoid clashes with other plugins.
+ *Everything below*

## NatCam Core 1.5b4
+ NatCam.OnStart is now called when cameras are changed and when the orientation is changed.
+ Added a unified orientation pipeline for captured photos.
+ Added NatCamPreview component for properly displaying textures on UI panels with scaling and orientation.
+ Added NatCamFocuser component for detecting focus gestures.
+ Added ability to mirror image in Utilities.RotateImage.
+ Added bitcode support on iOS.
+ Added support for zooming on a large amount of iOS and Android devices.
+ Added DeviceCamera.MaxZoomRatio.
+ Added tap-to-focus functionality to MiniCam example using NatCamFocuser.
+ Improved NatCamFocuser by adding StartTracking(), StartTracking(FocusMode), and StopTracking() API.
+ Refactored NatCam.OnPreviewStart, INatCam.OnPreviewStart, and NatCamBehaviour.OnPreviewStart to OnStart.
+ Refactored NatCam.OnPreviewUpdate, INatCam.OnPreviewUpdate, and NatCamBehaviour.OnPreviewUpdate to OnFrame.
+ Fixed crash on Android when NatCam is unable to open the camera.
+ Fixed rare crash on Android when app lost focus with multithreaded rendering disabled.
+ Fixed exception on Android when NatCam.Camera getter is called before NatCam.Camera has been set.
+ Fixed DeviceCamera.SetFocus focus point being inverted on the Y axis.
+ Fixed bug on Android where switching cameras caused preview to freeze on older devices.
+ Fixed bug on Android where DeviceCamera.TorchMode getter always returned Off.
+ Fixed bug where preview was incorrectly oriented after NatCam.Release() was called.
+ Fixed bug on Android where preview would flip briefly when camera was switched.
+ Fixed bug on NatCamLegacy where preview did not update after Pause() was called.
+ Fixed bug on Android where setting a camera that did not exist caused a hard crash.
+ Fixed bug on iOS where suspending the app caused system sounds to be distorted.
+ Fixed bug on iOS where pausing the preview caused the orientation to be incorrect.
+ Fixed 'Camera is being used after Camera.release() was called' exception on Android.
+ Fixed bug on Android where z-sorting was affected by the preview.
+ Fixed build error when building Android project with NatCam and Android Ultimate Plugin.
+ Deprecated NatCamScaler component.
+ Deprecated NatCamView component.
+ Deprecated NatCamZoomer component.
+ Deprecated NatCam.HasPermissions. Use NatCam.Implementation.HasPermissions instead.
+ Deprecated DeviceCamera.IsZoomSupported. Instead, check that DeviceCamera.MaxZoomRatio > 1.
+ NatCamView2D shader has been refactored to NatCamTransform2D.
+ IDispatch has been further modularized.
+ IDispatch will no more invoke a delegate more than once per Update.
+ *Everything below*

## NatCam Pro 1.5b4
+ Added ReadablePreview flag on NatCamAndroid. This is a workaround for the GPU driver bug that caused lag on the S7 Edge.
+ Added bitcode support on iOS.
+ Added NatCamToMatHelper script for OpenCV/DLibFaceLandmarkDetector.
+ Fixed crash when NatCam.StopRecording is called on Android.
+ Fixed null reference exception when NatCam.PreviewBuffer is called on NatCamLegacy.
+ Deprecated Utilities.SaveVideoToGallery. Use NatCam.Implementation.SaveVideo(string, SaveMode) instead.
+ Fixed incorrect colors when using VisionCam example on iOS.
+ Deprecated MotionCam example.
+ *Everything below*

## NatCam Core 1.5b3
+ NatCam now supports autorotation.
+ Made preview rendering faster on Android.
+ Made PhotoCallback return orientation information.
+ Added NatCamScaler component for scaling the preview to avoid stretching.
+ Added NatCamZoomer component for zooming the preview with input gestures.
+ Added NatCamFocuser component for focusing the camera at touch points.
+ Added DeviceCamera.Framerate getter.
+ Added Orientation struct for photo orientation.
+ Added NatCamView component for orienting captured photos using a GPU shader.
+ Added NatCamView2D shader for GPU-accelerated image transformation.
+ Added Utilities.RotateImage(...) for rotating an image in system memory.
+ Added ScaleMode.Letterbox for letterbox scaling.
+ Added NatCam.HasPermissions boolean.
+ Fixed bug where screen flickered when running on Android without multithreaded rendering.
+ Fixed bug on Android where calling Play() after Release() or pausing and suspending the app does not work.
+ Fixed bug on Android where camera does not pause when Pause() is called.
+ Fixed crash when NatCam.Release() is called on Android.
+ Fixed FocusMode.MacroFocus not working on Android.
+ Fixed DeviceCamera.FocusMode getter causing an error.
+ Fixed bug where DeviceCamera.SetFocus(..) ignored focus mode completely.
+ Fixed bug where OnPreviewStart is not called when preview is resumed on Android.
+ Fixed bug where setting high framerates rarely worked on iOS.
+ Fixed bug where preview stretched when using NatCamScaler and autorotation.
+ Fixed resource leak when NatCam.Release() is called after NatCam.Pause() on NatCamLegacy.
+ Fixed memory leak when taking photos with the MiniCam example.
+ Fixed potential namespace conflicts.
+ Fixed resolution set for one camera being used by another on NatCamLegacy.
+ Fixed IL2CPP build support on Android.
+ Deprecated INatCamMobile.HasPermissions.
+ Deprecated INatCamMobile interface.
+ Deprecated NatCamTransformation2D shader. Use NatCamView2D instead.
+ Renamed Minigram example to MiniCam.
+ Renamed StartingOff example to PlainCam.
+ Refactored INatCamDispatch to IDispatch.
+ Refactored NatCamUtilities to Utilities.
+ Refactored ScaleMode.FixedHeightVariableWidth to ScaleMode.AdjustWidth.
+ Refactored ScaleMode.FixedWidthVariableHeight to ScaleMode.AdjustHeight.
+ Made NatCamLegacy call OnPreviewStart only when Preview is not 16x16 (usually encountered on macOS).
+ Implemented IDisposable interface for IDispatch.
+ *Everything below*

## NatCam Pro 1.5b3
+ Added NatCam.PreviewMatrix(..) to greatly improve memory efficiency (so as not to allocate each time it is called).
+ Added NatCam.PreviewFrame(..) to greatly improve memory efficiency (so as not to allocate each time it is called).
+ Added VisionCam example to demonstrate using NatCam with OpenCVForUnity.
+ Added MotionCam example to demonstrate using NatCam.PreviewFrame.
+ Exposed bitrates for configuring video encoders.
+ Deprecated NatCam.PreviewMatrix property.
+ Deprecated NatCam.PreviewFrame property.
+ Fixed crash when NatCam.StopRecording is called on iOS.
+ Fixed NatCam.PreviewMatrix having incorrect dimensions.
+ *Everything below*

## NatCam Core 1.5f2
+ Added master switches to enable and disable the Extended and Professional spec in NatCamLinker.
+ Added support for getting DeviceCamera.PhotoResolution on NatCamLegacy.
+ Added support for more Android devices by making camera requirement optional.
+ Fixed preview not showing on a large number of Android devices.
+ Fixed memory leak when calling NatCam.Play() when preview is paused on iOS.
+ Fixed crash on start when running on some Android devices running OpenGLES 2.
+ Fixed bug where camera did not switch on Android.
+ Fixed OnPreviewStart not being called when NatCam.Play() is called after NatCam.Pause().
+ Fixed DeviceCamera.PreviewResolution returning wrong values on NatCamLegacy.
+ Fixed iOS 10 crash by adding NSMicrophoneUsageDescription.
+ Fixed endless refresh when using plugins that used custom platform macros like Cross Platform Native Plugins.
+ Fixed 'requested build target group (15) doesn't exist' error in NatCamLinker.
+ *Everything below*

## NatCam Pro 1.5f2
+ Added ReplayCam video recording example with recorded video playback.
+ Added flag to specify whether audio ppermission should be requested and if audio should be recorded.
+ Fixed crash when preview starts on Android devices running on OpenGLES 3.
+ Fixed microphone hardware requirement on Android.
+ *Everything below*

## NatCam Core 1.5f1
+ Added three API specifications: Core, Extended, and Professional for developers to choose from.
+ Completely rebuilt API to prepare for supporting more platforms.
+ Greatly improved speed of NatCam and Unity on Android.
+ Greatly improved speed of capturing photos especially on Android.
+ Made API easier to use by removing extraneous functions.
+ Added NatCamBehaviour component for quickly using NatCam.
+ Added FocusMode.SoftFocus for soft autofocus especially in VR applications on Android.
+ Added FocusMode.MacroFocus for focusing on up-close objects like barcodes on Android.
+ Added NatCamView2D shader for GPU-accelerated image transformation.
+ Added NatCam.CapturePhoto(PhotoCallback).
+ Added NATCAM_CORE, NATCAM_EXTENDED, NATCAM_PROFESSIONAL macros for specification-dependent compilation.
+ Added NATCAM_[version number] macro for version-dependent compilation. In 1.5, it is NATCAM_15.
+ Added support for more tablets on the Play store by making flash and focus capabilities optional.
+ Added a blacklist to keep track of devices that don't work.
+ Added support for more Android devices by making camera requirement optional.
+ Deprecated NatCam.Initialize(...). NatCam will initialize itself however it needs to.
+ Deprecated UnitygramBase component for NatCamBehaviour component.
+ Deprecated NatCam.ExecuteOnPreviewStart(PreviewCallback). Use NatCam.OnPreviewStart instead.
+ Deprecated NatCam.CapturePhoto() and NatCam.OnPhotoCapture event.
+ Dropped captured photo correction for app orientation. Now all captured photos are in landscape left orientation.
+ Fixed Android N compatibility.
+ Fixed iOS 10 compatibility by adding NSCameraUsageDescription.
+ Fixed rare crash immediately app is suspended on Android.
+ Fixed rare crash when calling NatCam.Release() on Android.
+ Fixed EXC_BAD_ACCESS crash when switching cameras on iOS.
+ Fixed preview incorrectly rotating when app is using fixed orientation on iOS.
+ Fixed build error on Android because of targetSDKVersion.
+ Fixed OnPreviewStart not being invoked when camera was played after being paused.
+ Fixed OnPreviewStart being invoked too soon after camera was played after being paused.
+ Fixed OnPreviewUpdate being called too frequently on NatCam Legacy (WebCamTexture).
+ Fixed preview flipping momentarily when switching cameras on Android.
+ Fixed bug on Android where photo resolution could only be set before NatCam.Play() is called.
+ Fixed bug on Android where some devices fail to resume the preview after the app is suspended.
+ Fixed bug on Android where calling CapturePhoto() on cameras that did not support flash mode failed.
+ Fixed bug on iOS where setting exposure mode to Auto did not work properly.
+ Fixed bug on iOS where DeviceCamera.VerticalFOV returned horizontal FOV.
+ Fixed bug on iOS where preview orientation might be incorrect when device was face up or down.
+ Fixed rare issue where preview might freeze after some seconds.
+ Fixed camera preview stopping when camera property was set on a camera that is not active.
+ Fixed stackTraceLogType warning on Unity 5.4 and above.
+ Rebuilt documentation.
+ Refactored NatCam.ActiveCamera to NatCam.Camera.
+ Reimplemented control pipeline on Android to be cleaner and more efficient.
+ Removed FocusMode.HybridFocus. Use bitwise OR instead.
+ Reduced the amount of text that NatCam logs.
+ Changed Android API minimum requirement to API level 15.
+ Changed ResolutionPreset.HighestResolution to default to FullHD when calling DeviceCamera.SetPreviewResolution(ResolutionPreset).

## NatCam Pro 1.5f1
+ Video recording is now available on platforms that support it.
+ Added NatCam.PreviewBuffer(...) that works on all platforms, even when using WebCamTexture.
+ Added Utilities.NatCamUtilities.SaveVideoToGallery to save recorded videos to device gallery.
+ On Android, the native preview update and preview data dimensions now respects the orientation of the app.
+ On Android, NatCam.PreviewFrame is no more skewed depending on app orientation.
+ Deprecated the concept of Readable preview. Preview data is now available on demand.
+ Deprecated OnNativePreviewUpdate event.
+ Deprecated ComponentBuffer enum.
+ Reimplemented NatCam.PreviewMatrix to be on demand like NatCam.PreviewFrame.
+ Renamed OPENCV_DEVELOPER_MODE macro to OPENCV_API

## NatCam 1.4f1:
*NOT RELEASED*

## NatCam 1.3:
+ Added universal barcode detection support. Now, barcodes can be detected in the editor and all other platforms.
+ Added Exposure control with DeviceCamera.ExposureBias, DeviceCamera.MinExposureBias and DeviceCamera.MaxExposureBias.
+ Added ExposureMode enum and DeviceCamera.ExposureMode.
+ Added Face detection with Face struct and NatCam.RequestFace().
+ Added DeviceCamera.SetPhotoResolution() + overloads.
+ Added DeviceCamera.ActivePhotoResolution.
+ Added a new, low-cost rendering pipeline on Android.
+ Removed rendering pipeline on iOS. This has increased performance especially for GPU-bound applications.
+ Reduced rendering pipeline memory usage on Android.
+ Removed NATCAM_DEVELOPER_MODE and component buffer access (Y and UV buffers) from rendering pipeline.
+ Fixed hanging and crashing on Samsung Galaxy S4, Nexus 1, and other Android devices with PowerVR SGX540 family GPU's.
+ Barcode detection now supports Unicode characters.
+ Made camera switching faster on Android.
+ Reimplemented NatCamPreviewScaler to be more stable.
+ Added NatCam.SaveToPhotos for saving Texture2D to the gallery or app album.
+ Added necessary checks and error logging for google_play_services when detecting barcodes and faces on Android.
+ Fixed bug on Android where camera must be manually focused before autofocus starts.
+ Fixed bug where NatCam becomes unresponsive when there is no camera in the scene.
+ Removed Verbose switch from NatCam.Initialize(), it is now a member variable (NatCam.Verbose = ...).
+ Deprecated BarcodeDetection switch in NatCam.Initialize() for MetadataDetection (which now includes faces).
+ Changed OnDetectedBarcode delegate template to take single barcode instead of list.
+ Fixed barcodes not being detected when the preview is resumed after calling Pause on Android.
+ Fixed FormatException when scanning some barcodes.
+ Fixed bug where NatCam.ExecuteOnPreviewStart invokes immediately after switching cameras.
+ Fixed bug where NatCam.PreviewMatrix is null when the OnPreviewStart event is broadcast.
+ Fixed bug where NatCamPreviewScaler will incorrectly stretch UI panel on iOS.
+ Added NatCam.IsInitialized property.
+ Renamed Unitygram example to Minigram.
+ Everything below.

## NatCam 1.2:
+ Completely rebuilt API to have a more object-oriented programming pattern, and to be much cleaner.
+ Immediate native-to-managed callbacks, deprecated UnitySendMessage for MonoPInvokeCallback with function pointers.
+ Added NativePreviewCallback and NatCamNativeInterface.EnableComponentUpdate() to get access to the raw luma (Y) and chroma (UV) buffers from the camera.
+ Added the NatCam rendering pipeline to NatCam iOS.
+ Added NatCamNativeInterface.DisableRenderingPipeline() to disable NatCam's native rendering pipeline.
+ Added NatCamPreviewGestures component for easy detection of focusing and zooming gestures.
+ Added UnitygramBase component for quickly implementing NatCam.
+ Removed NatCam.AutoTapToFocus, NatCam.AutoPinchToZoom.
+ Camera preview is now accessible through NatCam.Preview and returns a Texture (not Texture2D).
+ Camera switching on iOS and Android is now stable and responsive.
+ Added SetFramerate() in the DeviceCamera class and FrameratePreset enum.
+ Added HorizontalFOV, VerticalFOV, IsTorchSupported, IsFlashSupported, and IsZoomSupported in the DeviceCamera class.
+ Added DeviceCamera.Cameras list of cameras on the device, not just DeviceCamera.Front/RearCamera.
+ Added ability to specify NatCam interface (native or fallback) and NatCamInterface enum.
+ Added verboseMode switch to NatCam.Initialize() for debugging.
+ Added NatCam.RequestBarcodeDetection(), BarcodeRequest struct.
+ Added ability to use bitwise operators to request multiple barcode formats when creating barcode detection requests.
+ Added NatCam.HasPermissions to check if the app has camera permissions.
+ Deprecated DeviceCamera.SupportedResolutions and Resolution struct. Use ResolutionPreset instead.
+ Deprecated CapturePhoto overloads for NatCam.CapturePhoto(params PhotoCaptureCallback[] callbacks).
+ Removed '#if CALLIGRAPHY_DOC_GEN_MODE' conditional directives making code much cleaner.
+ Fix camera preview lagging on iOS.
+ OpenCV PreviewMatrix now updates from the native pixel buffer. This gives some memory savings and performance increase.
+ Captured photo is now the highest resolution that the camera supports by default.
+ Captured photo is now RGBA32 format. This means you can use Get/SetPixels(s), EncodeToJPG/PNG, and Apply.
+ Preview is now RGBA32 format on both iOS and Android.
+ Fixed "Error Creating Optimization Context" when using Readable preview on Galaxy S6 and Galaxy Tab.
+ Fixed rare scan line jitter when NatCam corrects padding with Readable preview on some Android devices.
+ Added ALLOCATE_NEW_PHOTO_TEXTURES macro for optimizing memory usage.
+ Added ALLOCATE_NEW_FRAME_TEXTURES macro for optimizing memory usage.
+ Fixed Android crash on Stop().
+ Removed NatCam detectTouchesForFocus and detectPinchToZoom for NatCamPreviewGestures component.
+ Fixed error when LoadLevel is called--something along the lines of "SendMessage: NatCamHelper not found".
+ Added editor-serialized variables for Unitygram example. Now, you can set Unitygram's variables from the editor instead of code.
+ Unitygram example is now a camera app featuring photo capture with flash, switching cameras, and barcode detection.
+ Automatically link required frameworks on iOS.
+ Deprecated OPTIMIZATION_USE_NATIVE_BUFFER macro, and as a result, direct support for Unity 5.2 has been stopped.
+ NatCamPreviewUIPanelScaler and NatCamPreviewUIPanelZoomer have been renamed to NatCamPreviewScaler and NatCamPreviewZoomer respectively.
+ Deprecated NatCam.Stop() for NatCam.Release().
+ Fixed NatCam.Release() (formerly 'Stop') being ignored when NatCam.Pause() is called immediately before.
+ Fixed NatCamPreviewScaler not correctly scaling HD and FullHD preview on iOS.
+ Fixed rotation and skewing when using CapturedPhoto with OpenCV.
+ Fixed memory leak when calling Release() (formerly named 'Stop') and Initialize() several times on iOS.
+ Fixed occasional tearing when running very high resolution previews on iOS and Android.
+ Completely rebuilt the documentation.
+ Added Easter Eggs on iOS.
+ Everything below.

## NatCam 1.1:
+ FocusMode enum. Now you can specify the active camera's focus mode from one of the four FocusModes: AutoFocus, TapToFocus, HybridFocus, and Off.
+ Orientation support. NatCam now supports all orientations. The preview will always be correctly rotated on all orientations.
+ Native Sources. iOS sources and Android Java sources are now included. Instructions on how to compile both are also included. Android C++ sources will be coming in the next release.
+ Native Plugin Callback for access to the camera preview data in native C-based code (C, C++, Objective-C, and C#). A code example for how to do this is included in the documentation (under NatCam>NatCamNativePluginUpdateEvent).
+ Torch while preview is running.
+ Everything below.

## NatCam 1.0:
+ Fluid camera preview on iOS and Android.
+ OpenCV and AR support on iOS and Android.
+ Autofocus.
+ Tap-to-Focus.
+ Zoom (Camera zoom on devices that support it, but a shader-accelerated digital zoom option is available on all devices).
+ Pinch-to-Zoom.
+ Machine Readable Code Detection (e.g QR Codes, ISBN Barcodes, and so on).
+ Capture Photos.
+ Saving captured photos to user devices (and an app album).
+ Flash.
+ Torch.
+ Front Camera Support.
+ Switching Cameras.
+ Getting Supported Camera Resolutions.
+ Android IL2CPP support (and iOS IL2CPP).
+ Low memory footprint.
+ Low CPU utilization.