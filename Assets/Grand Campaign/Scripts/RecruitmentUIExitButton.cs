using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecruitmentUIExitButton : MonoBehaviour
{
    public void Exit()
    {
        Player.Instance.SetTreeOpen(false);
        SceneManager.UnloadSceneAsync("GC_RecruitmentScene");
    }
}
