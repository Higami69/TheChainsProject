using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionMenuBuildingTreeButton : MonoBehaviour {
    public void Click()
    {
        Player.Instance.SetTreeOpen(true);
        Player.Instance.SelectedRegion.OpenBuildingTree();
    }
}
