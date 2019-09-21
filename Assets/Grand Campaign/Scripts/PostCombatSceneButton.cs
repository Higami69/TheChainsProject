using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostCombatSceneButton : MonoBehaviour {
    public void Click()
    {
        Player.Instance.SetTreeOpen(false);

        SceneManager.UnloadSceneAsync("GC_PostCombatScene");
    }
}
