using UnityEngine;
using UnityEngine.UI;
using NatCam;

public class ControlCam : MonoBehaviour {

    public RawImage rawImage; // Set this in the Editor
    private CameraDevice cameraDevice;

    void Start () {
        var cameraDevice = CameraDevice.GetDevices()[0];
        cameraDevice.PreviewResolution = (1920, 1080);
        cameraDevice.PhotoResolution = (4000, 3000);
        cameraDevice.Framerate = 60;
        // Start the preview
        cameraDevice.StartPreview(OnStart);
    }
    
    void OnStart (Texture2D preview) {
        // Display the preview
        rawImage.texture = preview;

        // Lock the camera's exposure
        cameraDevice.ExposureLock = true;
        // Set the exposure bias to the minimum supported // This will darken the preview
        cameraDevice.ExposureBias = cameraDevice.MinExposureBias;
        // Magnify the camera view 2x
        cameraDevice.ZoomRatio = 2.0f;
    }
    void OnFrame (long timestamp) {
        // You can perform this same login in `Update` or `FixedUpdate`, just make sure that `cameraDevice.IsRunning`
        // Focus on touch // We must normalize the touch position from screen to viewport ([0.0-1.0])
        foreach (var touch in Input.touches)
            cameraDevice.FocusPoint = (touch.position.x / Screen.width, touch.position.y / Screen.height);
    }
}