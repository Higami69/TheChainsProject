using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDrawer : MonoBehaviour {
    private static LineRenderer _Renderer;

    private void Awake()
    {
        _Renderer = GetComponent<LineRenderer>();
    }

    public static void ResetPoints(int newNrPoints)
    {
        _Renderer.positionCount = newNrPoints;
    }

    public static void AddPoint(int idx, Vector3 pos)
    {
        _Renderer.SetPosition(idx, pos);
    }
}
