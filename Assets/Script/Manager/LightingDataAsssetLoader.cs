using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingDataAssetLoader : MonoBehaviour
{
    public LightingDataAsset lightingDataAsset;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Lightmapping.lightingDataAsset = lightingDataAsset;
    }
}