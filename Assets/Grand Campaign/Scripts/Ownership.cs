using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ownership : MonoBehaviour {
    public Owner Owner;
}

public enum Owner
{
    PLAYER,
    ENEMY_1,
    CONTESTED //For Regions only
};

