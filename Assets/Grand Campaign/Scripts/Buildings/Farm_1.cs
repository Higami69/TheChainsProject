using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_1 : Building {
    public override void HandleCreation(Town town, Region region)
    {
        town.ChangeIncome(10.0f);
    }

    public override void HandleObsoletion(Town town, Region region)
    {
        town.ChangeIncome(-10.0f);
    }
    public override void HandleTurnEnd(Town townn, Region region)
    {
        MoneyManager.Instance.AddMoney(10.0f * region.IncomeMultiplier);
    }

    public override string GetName()
    {
        return "Small Farm";
    }

    public override string GetDescription()
    {
        return "A small farm\nthat produces\n10 money per turn.";
    }
}
