using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : Building {
    public override void HandleCreation(Town town, Region region)
    {
    }
    public override void HandleObsoletion(Town town, Region region)
    {
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
    }

    public override string GetDescription()
    {
        return "Placeholder";
    }

    public override string GetName()
    {
        return "Name";
    }
}
