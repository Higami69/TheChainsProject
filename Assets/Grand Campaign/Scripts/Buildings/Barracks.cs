using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building {
    public override void HandleCreation(Town town, Region region)
    {
        town.EnableUnitForTraining(UnitDatabase.Instance.GetUnit("Brute"));
        town.EnableUnitForTraining(UnitDatabase.Instance.GetUnit("Slinger"));
    }
    public override void HandleObsoletion(Town town, Region region)
    {
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
    }

    public override string GetName()
    {
        return "Barracks";
    }
    public override string GetDescription()
    {
        return "Enables recruitment of Brutes and Slingers.\nBoth are offensive units.";
    }
}
