using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionMenuMasterButton : MonoBehaviour {
    public string SceneName;

    public void Click()
    {
        RegionMenuMasterSelector.SwapScene(SceneName);
    }
}
