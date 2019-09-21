using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingTreeUIExitButton : MonoBehaviour {

    public void CloseUI()
    {
        Player.Instance.SetTreeOpen(false);
        SceneManager.UnloadSceneAsync("GC_BuildingTreeScene");
    }
}
