using UnityEngine;

public class ScreenshotManager : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.F2)) {
            ScreenCapture.CaptureScreenshot(@"E:\EMIT\ScreenShot_" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png", 2);
        }
    }
}
