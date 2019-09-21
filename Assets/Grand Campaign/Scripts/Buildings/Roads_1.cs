using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads_1 : Building {
    public override void HandleCreation(Town town, Region region)
    {
        region.MovementMultiplier += 0.05f;
    }

    public override void HandleObsoletion(Town town, Region region)
    {
        region.MovementMultiplier -= 0.05f;
    }

    public override void HandleTurnEnd(Town town, Region region)
    {
    }

    public override string GetName()
    {
        return "Dirt Roads";
    }

    public override string GetDescription()
    {
        return "Roads throughout the region.\nIncreases army movement speed by 5%";
    }
}
