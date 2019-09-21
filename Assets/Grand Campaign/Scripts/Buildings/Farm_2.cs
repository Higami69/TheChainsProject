using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_2 : Building {
    public override void HandleCreation(Town town, Region region)
    {
        town.ChangeIncome(20.0f);
    }
    public override void HandleObsoletion(Town town, Region region)
    {
        town.ChangeIncome(-20.0f);
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
        MoneyManager.Instance.AddMoney(20.0f * region.IncomeMultiplier);
    }

    public override string GetName()
    {
        return "Medium Farm";
    }

    public override string GetDescription()
    {
        return "An average farm\nthat produces\n20 money per turn.";
    }
}
