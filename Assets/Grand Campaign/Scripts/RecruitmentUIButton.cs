using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitmentUIButton : MonoBehaviour {
    public UnitData data;
    public void Click()
    {
        if (MoneyManager.Instance.GetCurrentMoney() > data.Cost)
        {
            Player.Instance.SelectedTown.Recruit(data);
            MoneyManager.Instance.RemoveMoney(data.Cost);
            TownMenuRecruitmentLoader.Reload();
        }
    }
}
