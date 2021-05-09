/* 
*   NatShare
*   Copyright (c) 2021 Yusuf Olokoba
*/


    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using NatSuite.Sharing;

    public class SaveTest : MonoBehaviour {
       
         public void Start () {
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
        }
    