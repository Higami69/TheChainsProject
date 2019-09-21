using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHouse : Building {

    public override void HandleCreation(Town town, Region region)
    {
        town.EnableUnitForTraining(UnitDatabase.Instance.GetUnit("Spearman"));
    }
    public override void HandleObsoletion(Town town, Region region)
    {
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
    }
    public override string GetName()
    {
        return "Guardhouse";
    }
    public override string GetDescription()
    {
        return "Enables spearmen to be recruited\n";
    }
}
