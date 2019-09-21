using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionMenuGeneralLoader : MonoBehaviour {
    private Text _Income, _IncomeMult, _MovMult, _Buildings;

    private void Start()
    {
        GameObject.Find("Name").GetComponent<Text>().text = Player.Instance.SelectedRegion.Name;
        _Income = GameObject.Find("Income").GetComponent<Text>();
        _IncomeMult = GameObject.Find("IncomeMultiplier").GetComponent<Text>();
        _MovMult = GameObject.Find("MovementMultiplier").GetComponent<Text>();
        _Buildings = GameObject.Find("BuildingsBuilt").GetComponent<Text>();
    }

    private void Update()
    {
        var player = Player.Instance;
        _Income.text = player.SelectedRegion.GetIncome().ToString();
        _IncomeMult.text = player.SelectedRegion.IncomeMultiplier.ToString();
        _MovMult.text = player.SelectedRegion.MovementMultiplier.ToString();
        _Buildings.text = player.SelectedRegion.GetBuildingsBeingBuilt().ToString();
    }
}
