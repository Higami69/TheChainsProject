using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownMenuRegionButton : MonoBehaviour {
    public void Click()
    {
        Player.Instance.OpenRegion();
        Player.Instance.SelectedRegion = Player.Instance.SelectedTown.GetRegion();
        Player.Instance.SelectedTown = null;

        TownMenuMasterSelector.Unload();
        SceneManager.UnloadSceneAsync("GC_TownMenuMaster");

        SceneManager.LoadSceneAsync("GC_RegionMenuMaster", LoadSceneMode.Additive);

        Player.Instance.SelectedRegion.gameObject.GetComponent<SelectableArea>().Select();
    }
}
