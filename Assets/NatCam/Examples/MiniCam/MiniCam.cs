/* 
*   NatCam
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCam.Examples {

    using UnityEngine;
    using UnityEngine.Android;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using NatSuite.Sharing;
    using System.IO;
    using System.Threading.Tasks;

   
    
    public class MiniCam : MonoBehaviour {

        [Header("UI")]
        public RawImage rawImage;
        public AspectRatioFitter aspectFitter;
        public Text flashText;
        public Button switchCamButton, flashButton;
        public Image checkIco, flashIco;
        public float ZoomRatio=0.0f;
        // private bool camAvailable;
        // private WebCamTexture backCam;
        
        private CameraDevice cameraDevice;
        // private CameraDevice[] cameras;
        // private int activeCamera = -1;
        private Texture previewTexture;
        private Texture2D photo;


        #region --Unity Messages--

        // Use this for initialization
        private void Start () {
            cameraDevice = CameraDevice.GetDevices()[0];
        cameraDevice.StartPreview(OnStart);
            // Request permissions
            // if (Application.platform == RuntimePlatform.Android && !Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            //     Permission.RequestUserPermission(Permission.Camera);
            //     yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.Camera));
            // }
            // // Start preview
			// cameras = CameraDevice.GetDevices();
            // if (cameras != null) {
            //     activeCamera = 0;
            //     cameraDevice.PreviewResolution = (1920, 1080);
            //     cameraDevice.PhotoResolution = (4000, 3000);
            //     cameraDevice.Framerate = 60;
            //     cameraDevice.StartPreview(OnStart);
            // }
            // else
            //     Debug.Log("User has not granted camera permission");          
            // if (CapturePhoto()=true){
            // Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            // // Get assets to share
            // var screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            // var basePath = Application.platform == RuntimePlatform.Android ? Application.persistentDataPath : Application.streamingAssetsPath;
            // var videoPath = Path.Combine(basePath, "animation.gif");
            // // Save to camera roll
            // var payload = new SavePayload("NatShare");
            // payload.AddImage(screenshot);
            // var success = payload.Commit();
            // Debug.Log($"Successfully saved items to camera roll: {success}");
            // }
            // else
            // checkIco.gameObject.SetActive(false);

        }
        #endregion

        
        #region --Callbacks--

        private void OnStart (Texture2D preview) {
            // Display the preview
            previewTexture = preview;
            rawImage.texture = preview;
            aspectFitter.aspectRatio = preview.width / (float)preview.height;
        //     cameraDevice.ExposureLock = true;
        // // Set the exposure bias to the minimum supported // This will darken the preview
        //     cameraDevice.ExposureBias = cameraDevice.MinExposureBias;
            cameraDevice.ZoomRatio = 0.0f;
            // Set flash to auto
            cameraDevice.FlashMode = FlashMode.Auto;
            UpdateFlashIcon();
        }
        
        private void OnPhoto (Texture2D photo) {            
            // Display the photo
            this.photo = photo;
            rawImage.texture = photo;
            aspectFitter.aspectRatio = photo.width / (float)photo.height;
            // Update UI
            checkIco.gameObject.SetActive(true);
            switchCamButton.gameObject.SetActive(false);
            flashButton.gameObject.SetActive(false);
        }

        private void OnView () {
            // Disable the check icon
            checkIco.gameObject.SetActive(false);
            // Display the preview
            rawImage.texture = previewTexture;
            // Scale the panel to match aspect ratios
            aspectFitter.aspectRatio = previewTexture.width / (float)previewTexture.height;
            // Enable the switch camera button
            switchCamButton.gameObject.SetActive(true);
            // Enable the flash button
            flashButton.gameObject.SetActive(true);
            // Free the photo texture
            Texture2D.Destroy(photo); photo = null;

        }
        #endregion
        
        
        #region --UI Ops--

        public virtual void CapturePhoto () {
            // Divert control if we are checking the captured photo
            if (!checkIco.gameObject.activeInHierarchy){
                cameraDevice.CapturePhoto(OnPhoto);
                Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            // Get assets to share
            var screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            var basePath = Application.platform == RuntimePlatform.Android ? Application.persistentDataPath : Application.streamingAssetsPath;
            var videoPath = Path.Combine(basePath, "animation.gif");
            // Save to camera roll
            var payload = new SavePayload("NatShare");
            payload.AddImage(screenshot);
            var success = payload.Commit();
            Debug.Log($"Successfully saved items to camera roll: {success}");
            }
 
            // Check captured photo
            else OnView();
        }
        
        // public void SwitchCamera () {
        //     cameraDevice.StopPreview();
        //     activeCamera = (activeCamera + 1) % cameras.Length;
        //     cameraDevice.StartPreview(OnStart);
        // }
        
        public void ToggleFlashMode () {
            // Set the active camera's flash mode
            if (cameraDevice.IsFlashSupported)
                switch (cameraDevice.FlashMode) {
                    case FlashMode.Auto: cameraDevice.FlashMode = FlashMode.On; break;
                    case FlashMode.On: cameraDevice.FlashMode = FlashMode.Off; break;
                    case FlashMode.Off: cameraDevice.FlashMode = FlashMode.Auto; break;
                }
            // Set the flash icon
            UpdateFlashIcon();
        }

        public void FocusCamera (BaseEventData e) {
            // Get the touch position in viewport coordinates
            var eventData = e as PointerEventData;
            RectTransform transform = eventData.pointerPress.GetComponent<RectTransform>();
            Vector3 worldPoint;
            if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(transform, eventData.pressPosition, eventData.pressEventCamera, out worldPoint))
                return;
            var corners = new Vector3[4];
            transform.GetWorldCorners(corners);
            var point = worldPoint - corners[0];
            var size = new Vector2(corners[3].x, corners[1].y) - (Vector2)corners[0];
            // Set the focus point
            cameraDevice.FocusPoint = (point.x / size.x, point.y / size.y);
        }
        #endregion


        #region --Utility--
        
        private void UpdateFlashIcon () {
            // Set the icon
            bool supported = cameraDevice.IsFlashSupported;
            flashIco.color = !supported || cameraDevice.FlashMode == FlashMode.Off ? (Color)new Color32(120, 120, 120, 255) : Color.white;
            // Set the auto text for flash
            flashText.text = supported && cameraDevice.FlashMode == FlashMode.Auto ? "A" : "";
        }
        #endregion

        public void sliderZoom(float zoom){
        cameraDevice.ZoomRatio=zoom;
    }

    }
    
    
}