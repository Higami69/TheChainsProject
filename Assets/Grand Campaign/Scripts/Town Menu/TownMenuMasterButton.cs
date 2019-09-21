using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMenuMasterButton : MonoBehaviour {
    public string SceneName;

    public void Click()
    {
        TownMenuMasterSelector.SwapScene(SceneName);
    }
}
