#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class ImageLoaderForEditor : MonoBehaviour
{
    public GameObject targetObject; 
    
    public static void LoadImageAndCreateMaterial()
    {
        string path = EditorUtility.OpenFilePanel("Load Image", "", "png,jpg,jpeg");
        if (path.Length != 0)
        {
            var texture = LoadTexture(path);
            if (texture != null)
            {
                string assetPath = SaveTextureAsAsset(texture, path);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    Material material = CreateMaterialWithTexture(assetPath);
                    if (material != null)
                    {
                        ImageLoaderForEditor loader = FindObjectOfType<ImageLoaderForEditor>();
                        if (loader != null && loader.targetObject != null)
                        {
                            ApplyMaterialToObject(loader.targetObject, material);
                        }
                        else
                        {
                            Debug.LogError("EditorImageLoader component is missing or targetObject is not assigned.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to create material from texture.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to save texture as an asset.");
                }
            }
        }
    }

    private static Texture2D LoadTexture(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
            return texture;

        Debug.LogError("Failed to load texture from file.");
        return null;
    }
    private static void ApplyMaterialToObject(GameObject obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial = material;
        }
        else
        {
            Debug.LogWarning("The target object does not have a Renderer component.");
        }
    }

    private static string SaveTextureAsAsset(Texture2D texture, string originalPath)
    {
        try 
        {
            string directoryPath = "Assets/Resources/Textures";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = Path.GetFileName(originalPath);
            string assetsRelativePath = Path.Combine(directoryPath, fileName);
            File.WriteAllBytes(assetsRelativePath, texture.EncodeToPNG());
            AssetDatabase.ImportAsset(assetsRelativePath);
            return assetsRelativePath;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving texture as an asset: {e.Message}");
            return null;
        }
    }

    private static Material CreateMaterialWithTexture(string texturePath)
    {
        try
        {
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = texture;
            string materialPath = Path.ChangeExtension(texturePath, ".mat");
            AssetDatabase.CreateAsset(material, materialPath);
            return material;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error creating material: {e.Message}");
            return null;
        }
    }
    
}
#endif