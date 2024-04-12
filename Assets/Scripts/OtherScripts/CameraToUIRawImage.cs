using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CameraToUIRawImage : MonoBehaviour
{
    public Camera secondCamera; 

    private void Start()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 16);
        rt.Create();
        
        secondCamera.targetTexture = rt;
        
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = rt;
    }
}