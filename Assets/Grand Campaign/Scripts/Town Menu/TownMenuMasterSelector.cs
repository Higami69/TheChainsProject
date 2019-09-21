using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownMenuMasterSelector : MonoBehaviour {
    private static string OpenScene = null;
    private static bool _IsReload = false;

    public static void SwapScene(string newScene)
    {
        if (!_IsReload && OpenScene == newScene) return;
        if(OpenScene == "GC_TownMenuArmy" && Player.Instance.IsUnitStatsScreenOpen)
        {
            Player.Instance.IsUnitStatsScreenOpen = false;
            SceneManager.UnloadSceneAsync("GC_UnitStats");
        }
        if(newScene == "GC_TownMenuArmy")
        {
            Player.Instance.SelectedArmy = Player.Instance.SelectedTown.GetGarrison();
            if(Player.Instance.SelectedArmy.GetSize() > 0) Player.Instance.SelectedArmy.DisplayRange();
        }
        else if(OpenScene == "GC_TownMenuArmy")
        {
            Player.Instance.SelectedArmy = null;
            ArmyRangeDisplay.Instance.SetSize(0);
        }

        if(OpenScene != null) SceneManager.UnloadSceneAsync(OpenScene);
        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);
        OpenScene = newScene;
    }

    public static void Unload()
    {
        if(OpenScene != null) SceneManager.UnloadSceneAsync(OpenScene);
        if(OpenScene == "GC_TownMenuArmy")
        {
            ArmyRangeDisplay.Instance.SetSize(0);
            Player.Instance.SelectedArmy = null;
        }
        OpenScene = null;
    }

    public static void Reload()
    {
        _IsReload = true;
        SwapScene("GC_TownMenuGeneral");
        _IsReload = false;
    }

    private void Start()
    {
        SwapScene("GC_TownMenuGeneral");
    }
}
