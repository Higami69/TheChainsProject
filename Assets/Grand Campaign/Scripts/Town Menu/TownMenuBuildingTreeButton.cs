using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMenuBuildingTreeButton : MonoBehaviour {
    public void Click()
    {
        Player.Instance.SetTreeOpen(true);
        Player.Instance.SelectedTown.OpenBuildingTree();
    }
}
