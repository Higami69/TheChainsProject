using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishery_1 : Building{
    public override void HandleCreation(Town town, Region region)
    {
        town.ChangeIncome(5.0f);
    }
    public override void HandleObsoletion(Town town, Region region)
    {
        town.ChangeIncome(-5.0f);
    }
    public override void HandleTurnEnd(Town town, Region region)
    {
        MoneyManager.Instance.AddMoney(5.0f * region.IncomeMultiplier);
    }

    public override string GetName()
    {
        return "Fishing Dock";
    }

    public override string GetDescription()
    {
        return "A single dock used\nfor fishing, produces\n5 money per turn";
    }
}
