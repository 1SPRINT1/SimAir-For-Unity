using UnityEngine;
using System.IO;

// Убедитесь, что вы импортировали плагин NativeFilePicker в ваш проект
public class ImageLoaderForRuntime : MonoBehaviour
{
    public Renderer targetRenderer; // Ссылка на Renderer, куда будет применена текстура

    // Вызывается, когда пользователь нажимает на кнопку для выбора изображения
    public void PickImage()
    {
        // Проверяем, доступен ли выбор файлов
        if (NativeFilePicker.IsFilePickerBusy())
            return;

        // Запускаем выбор файла
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                // Загружаем текстуру
                Texture2D texture = LoadTextureFromFile(path);
                if (texture != null)
                {
                    // Применяем текстуру к Renderer
                    ApplyTextureToRenderer(targetRenderer, texture);
                }
            }
        });
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2); // Создаем текстуру с минимальным размером, так как она будет перезаписана
        if (texture.LoadImage(fileData)) // Загружаем изображение и применяем его к текстуре
            return texture;

        Debug.LogError("Failed to load texture from file.");
        return null;
    }

    private void ApplyTextureToRenderer(Renderer renderer, Texture2D texture)
    {
        if (renderer != null)
        {
            renderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Renderer is not assigned or is null.");
        }
    }
}
