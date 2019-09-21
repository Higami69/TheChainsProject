using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeCompany_3 : Building {
    public override void HandleCreation(Town town, Region region)
    {
        region.IncomeMultiplier += 0.1f;
    }

    public override void HandleObsoletion(Town town, Region region)
    {
        region.IncomeMultiplier -= 0.1f;
    }

    public override void HandleTurnEnd(Town town, Region region)
    {
    }

    public override string GetName()
    {
        return "Company";
    }

    public override string GetDescription()
    {
        return "Controls trade within the region.\nIncreases all income by 10%";
    }
}
