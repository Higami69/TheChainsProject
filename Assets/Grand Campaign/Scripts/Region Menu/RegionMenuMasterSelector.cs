using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionMenuMasterSelector : MonoBehaviour {
    private static string _OpenScene = null;

    public static void SwapScene(string newScene)
    {
        if (_OpenScene == newScene) return;

        if(_OpenScene != null) SceneManager.UnloadSceneAsync(_OpenScene);
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        _OpenScene = newScene;
    }

    public static void Unload()
    {
        if (_OpenScene != null) SceneManager.UnloadSceneAsync(_OpenScene);
        _OpenScene = null;
    }

    private void Start()
    {
        SwapScene("GC_RegionMenuGeneral");
    }
}
