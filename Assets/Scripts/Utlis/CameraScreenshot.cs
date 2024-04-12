using UnityEngine;
using System.Collections;
using System.IO;

public class CameraScreenshot : MonoBehaviour
{
    public Camera cameraToCapture;

    private void Start()
    {
        StartCoroutine(CaptureScreenshotRoutine());
    }

    private IEnumerator CaptureScreenshotRoutine()
    {
        string folderPath = "Assets/Resources/SavedImages"; 
        
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        while (true)
        {
            yield return new WaitForSeconds(1f); 

            // Формируем имя файла с таймстампом
            string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string filename = "Screenshot_" + timestamp + ".png";
            string fullPath = Path.Combine(folderPath, filename);
            
            CaptureScreenshot(fullPath);
        }
    }

    private void CaptureScreenshot(string filePath)
    {
        RenderTexture renderTexture = cameraToCapture.targetTexture;
        
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraToCapture.targetTexture = renderTexture;
            cameraToCapture.Render();
        }
        
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(512, 512, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
        screenshot.Apply();
        
        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        
        if (cameraToCapture.targetTexture == renderTexture)
        {
            cameraToCapture.targetTexture = null;
            Destroy(renderTexture);
        }
        
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        
        Destroy(screenshot);
        Destroy(renderTexture); 

        Debug.Log("Скриншот сохранен: " + filePath);
    }
}
