using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CameraToUIRawImage : MonoBehaviour
{
    public Camera secondCamera; 

    private void Start()
    {
        // Создаем новую RenderTexture
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 16);
        rt.Create();

        // Назначаем RenderTexture камере
        secondCamera.targetTexture = rt;

        // Получаем компонент RawImage и устанавливаем RenderTexture
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = rt;
    }
}