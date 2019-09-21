using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestFullSceneExitButton : MonoBehaviour {

    public void Click()
    {
        SceneManager.LoadSceneAsync("GC_QuestScene", LoadSceneMode.Additive);
        Player.Instance.SetTreeOpen(false);
        SceneManager.UnloadSceneAsync("GC_QuestFullScene");
    }
}
