using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeCompany_1 : Building {

    public override void HandleCreation(Town town, Region region)
    {
        region.IncomeMultiplier += 0.03f;
    }

    public override void HandleObsoletion(Town town, Region region)
    {
        region.IncomeMultiplier -= 0.03f;
    }
    public override void HandleTurnEnd(Town townn, Region region)
    {
    }

    public override string GetName()
    {
        return "Trading Post";
    }

    public override string GetDescription()
    {
        return "Controls trade within the region.\nIncreases all income by 3%";
    }
}
