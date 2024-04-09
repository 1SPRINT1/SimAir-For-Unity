using UnityEngine;
using System.Collections;
using System.IO;

public class CameraScreenshot : MonoBehaviour
{
    public Camera cameraToCapture;

    private void Start()
    {
        // Запускаем корутину для периодического создания скриншотов
        StartCoroutine(CaptureScreenshotRoutine());
    }

    private IEnumerator CaptureScreenshotRoutine()
    {
        // Путь к папке сохранения
        string folderPath = "Assets/Resources/SavedImages"; 
        
        // Создаем папку, если она не существует
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        while (true)
        {
            yield return new WaitForSeconds(1f); // Подождать 1 секунду

            // Формируем имя файла с таймстампом
            string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string filename = "Screenshot_" + timestamp + ".png";
            string fullPath = Path.Combine(folderPath, filename);

            // Вызываем метод создания скриншота
            CaptureScreenshot(fullPath);
        }
    }

    private void CaptureScreenshot(string filePath)
    {
        RenderTexture renderTexture = cameraToCapture.targetTexture;

        // Если у камеры нет RenderTexture, создаем временный
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraToCapture.targetTexture = renderTexture;
            cameraToCapture.Render();
        }

        // Активируем RenderTexture для чтения пикселей
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(512, 512, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
        screenshot.Apply();

        // Сбрасываем RenderTexture
        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;

        // Если создавался временный RenderTexture, освобождаем его
        if (cameraToCapture.targetTexture == renderTexture)
        {
            cameraToCapture.targetTexture = null;
            Destroy(renderTexture);
        }

        // Сохраняем скриншот в файл
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        // Освобождаем ресурсы
        Destroy(screenshot);
        Destroy(renderTexture); // Освободить временный RenderTexture

        Debug.Log("Скриншот сохранен: " + filePath);
    }
}
