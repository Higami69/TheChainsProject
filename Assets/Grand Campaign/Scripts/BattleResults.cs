using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResults : MonoBehaviour
{
    private static BattleResults _Instance;
    public static BattleResults Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        _Instance = this;
    }

    public bool Result;
    public int PlayerArmySize, EnemyArmySize;
}
