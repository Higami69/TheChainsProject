using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyRangeDisplay : MonoBehaviour {
    private static ArmyRangeDisplay _Instance;
    public static ArmyRangeDisplay Instance
    {
        get
        {
            return _Instance;
        }
    }

    private const float yPos = -0.49f;

    private void Awake()
    {
        _Instance = this;
    }

    public void SetSize(float radius)
    {
        transform.localScale = new Vector3(radius * 2, radius * 2);
    }

    public void SetPosition(Vector3 pos)
    {
        pos.y = yPos;
        transform.position = pos;
    }
}
