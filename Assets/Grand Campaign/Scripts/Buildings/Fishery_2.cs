using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishery_2 : Building {
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
        return "Fishing Port";
    }

    public override string GetDescription()
    {
        return "A small port for\nfishing boats to dock.\nProduces 20 money per turn.";
    }
}
