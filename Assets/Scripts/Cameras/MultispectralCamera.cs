using UnityEngine;
public class MultispectralCamera : MonoBehaviour
{
    public Camera[] spectralCameras; // Массив камер для разных спектров
    public RenderTexture[] spectralRenderTextures; // Массив RenderTextures для каждой камеры

    void Start()
    {
        // Проверяем, соответствует ли количество RenderTextures количеству камер
        if (spectralCameras.Length != spectralRenderTextures.Length)
        {
            Debug.LogError("Количество камер и RenderTextures должно совпадать!");
            return;
        }

        // Настраиваем каждую камеру для рендеринга в соответствующий RenderTexture
        for (int i = 0; i < spectralCameras.Length; i++)
        {
            if (spectralCameras[i] != null && spectralRenderTextures[i] != null)
            {
                spectralCameras[i].targetTexture = spectralRenderTextures[i];
            }
            else
            {
                Debug.LogError("Камера или RenderTexture не установлены!");
            }
        }
    }
}
