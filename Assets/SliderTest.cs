using UnityEngine;
    using UnityEngine.Android;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using System.Collections;
    using NatCam;

public class SliderTest : MonoBehaviour
{
    public float ZoomRatio=1.0f;
    private CameraDevice[] cameras;  
        private int activeCamera = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnStart (Texture2D preview) {
            cameras[activeCamera].ZoomRatio = 1.0f;

    }
    public void sliderZoom(float zoom){
        ZoomRatio=zoom;
    }

}
