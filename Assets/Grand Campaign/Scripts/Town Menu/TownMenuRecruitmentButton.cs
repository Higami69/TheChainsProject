using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownMenuRecruitmentButton : MonoBehaviour {
    public void Click()
    {
        UnitListToLoad.Instance = Player.Instance.SelectedTown.GetTrainableList();
        Player.Instance.SetTreeOpen(true);
        SceneManager.LoadScene("GC_RecruitmentScene", LoadSceneMode.Additive);
    }
}
