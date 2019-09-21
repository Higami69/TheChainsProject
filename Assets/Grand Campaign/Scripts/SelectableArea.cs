using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableArea : MonoBehaviour {
    public List<Vector3> _Area;

    public void Select()
    {
        SelectionDrawer.ResetPoints(_Area.Count);
        for(int i = 0; i < _Area.Count; i++)
        {
            SelectionDrawer.AddPoint(i, _Area[i]);
        }
    }
}
