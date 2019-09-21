using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_3 : Building {
    public override void HandleCreation(Town town, Region region)
    {
        town.ChangeIncome(30.0f);
    }
    public override void HandleObsoletion(Town town, Region region)
    {
        town.ChangeIncome(-30.0f);
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
        MoneyManager.Instance.AddMoney(30.0f * region.IncomeMultiplier);
    }

    public override string GetName()
    {
        return "Large Farm";
    }

    public override string GetDescription()
    {
        return "A large farm\nthat produces\n30 money per turn.";
    }
}
