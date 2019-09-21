using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMenuGeneralLoader : MonoBehaviour {
    private Text _Income, _Buildings, _Recruits, _Army;
    public GameObject RegionButton;
	void Start () {
        GameObject obj = GameObject.Find("Name");
        obj.GetComponent<Text>().text = Player.Instance.SelectedTown.Name;
        _Income = GameObject.Find("Income").GetComponent<Text>();
        _Buildings = GameObject.Find("BuildingsBuilt").GetComponent<Text>();
        _Recruits = GameObject.Find("RecruitAmount").GetComponent<Text>();
        _Army = GameObject.Find("ArmySize").GetComponent<Text>();
	}

    private void Update()
    {
        var player = Player.Instance;
        if (player.SelectedTown == null) return;

        if (player.SelectedTown.GetRegion().IsUnlocked() && player.SelectedTown.GetRegion().GetOwner() == Owner.PLAYER)
        {
            RegionButton.SetActive(true);
        }

        _Income.text = player.SelectedTown.GetIncome().ToString();
        _Buildings.text = player.SelectedTown.GetBuildingsBeingBuilt().ToString();
        _Recruits.text = player.SelectedTown.GetRecruitmentQueue().Count.ToString();
        _Army.text = player.SelectedTown.GetGarrison().GetSize().ToString();
    }
}
