using System.IO;
using NativeFilePickerNamespace;
using UnityEngine;

public class ImageLoaderAndroid : MonoBehaviour
{
    public Renderer targetRenderer; 

    public void PickImageAndLoadMaterial()
    {
        if (NativeFilePicker.IsFilePickerBusy())
            return;
        
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                Texture2D texture = LoadTextureFromFile(path);
                if (texture == null)
                {
                    Debug.LogError("Не удалось загрузить текстуру из файла.");
                    return;
                }
                
                if (targetRenderer != null)
                {
                    targetRenderer.material.mainTexture = texture;
                }
            }
        });

        if (permission == NativeFilePicker.Permission.Denied)
            Debug.LogWarning("Доступ к файлам был отклонен пользователем.");
        else if (permission == NativeFilePicker.Permission.ShouldAsk)
            Debug.LogWarning("Необходимо запросить разрешение на доступ к файлам.");
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
            return texture;

        return null;
    } 
}
