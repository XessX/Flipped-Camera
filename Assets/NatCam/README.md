# NatCam API
NatCam provides a clean, functional, and extremely performant API for accessing and controlling device cameras. The API is designed to be device camera-oriented. Hence all operations--whether it be introspection, running the preview, or capturing a photo--happen directly on `CameraDevice` instances.

Using NatCam is as simple as acquiring a camera device and using it:
```csharp
var cameraDevice = CameraDevice.GetDevices()[0];
cameraDevice.StartPreview(OnStart);
```

The camera starts running and the client-provided callback is invoked with the preview texture:
```csharp
void OnStart (Texture2D preview) {
    // Display the camera preview on a RawImage
    rawImage.texture = preview;
}
```

## Camera Control
NatCam features a full camera control pipeline for utilizing camera functionality such as focusing, zooming, exposure, and so on. All these properties are in the `CameraDevice` class. For example:
```csharp
cameraDevice.ExposureBias = 1.3f;
```

## Capturing Photos
NatCam also allows for high-resolution photo capture from the camera. To do so, simply call the `CapturePhoto` method with an appropriate callback to accept the photo texture:
```csharp
cameraDevice.CapturePhoto(OnPhoto);

void OnPhoto (Texture2D photo) {
    // Do stuff with the photo...
    ...
    // Remember to release the texture when you are done with it so as to avoid memory leak
    Texture2D.Destroy(photo); 
}
```

## Using NatCam with OpenCV
NatCam supports OpenCV with the [OpenCVForUnity](https://assetstore.unity.com/packages/tools/integration/opencv-for-unity-21088) package. Check out the [official examples](https://github.com/EnoxSoftware/NatCamWithOpenCVForUnityExample). Using NatCam with OpenCV is pretty easy. On every frame, simply copy the preview texture into an `OpenCVForUnity.Mat`:
```csharp
void Update () {
    // Create a matrix
    var previewMatrix = new Mat(previewTexture.height, previewTexture.width, CvType.CV_8UC4);
    // Copy the preview data into the matrix
    Utils.fastTexture2DToMat(previewTexture, previewMatrix);
    // Use the preview matrix
    // ...
}
```

___

With the simplicity of NatCam, you have the power and speed to create interactive, responsive camera apps. Happy coding!

## Requirements
- Unity 2018.3+
- Android API level 21+
- iOS 11+

## Tutorials
1. [Starting Off](https://medium.com/@olokobayusuf/natcam-tutorial-series-1-starting-off-dc3990f5dab6)
2. [Controls](https://medium.com/@olokobayusuf/natcam-tutorial-series-2-controls-d2e2d0738223)
3. [Photos](https://medium.com/@olokobayusuf/natcam-tutorial-series-3-photos-e28361b83cf8)

## Quick Tips
- Please peruse the included scripting reference in the `Docs` folder.
- To discuss or report an issue, visit Unity forums [here](http://forum.unity3d.com/threads/natcam-device-camera-api.374690/).
- Check out more NatCam examples on Github [here](https://github.com/olokobayusuf?tab=repositories).
- Contact me at [olokobayusuf@gmail.com](mailto:olokobayusuf@gmail.com).

Thank you very much!