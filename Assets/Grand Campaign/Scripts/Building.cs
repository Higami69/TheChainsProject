using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Building : MonoBehaviour {
    abstract public void HandleCreation(Town town, Region region);
    abstract public void HandleObsoletion(Town town, Region region);
    abstract public void HandleTurnEnd(Town town, Region region);
    abstract public string GetName();
    abstract public string GetDescription();
}
