using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    private List<Region> _Regions;
    private List<OverworldArmy> _Armies;

    private static TurnManager _Instance;
    public static TurnManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        _Instance = this;

        _Regions = new List<Region>();
        foreach(var obj in GameObject.FindGameObjectsWithTag("Region"))
        {
            _Regions.Add(obj.GetComponent<Region>());
        }

        _Armies = new List<OverworldArmy>();
    }

    public void EndTurn()
    {
        foreach(var region in _Regions)
        {
            region.HandleTurnEnd();
        }
        foreach(var army in _Armies)
        {
            army.HandleTurnEnd();
        }

        if (Player.Instance.SelectedArmy != null)
        {
            Player.Instance.SelectedArmy.DisplayRange();
        }

        TownMenuBuildingLoader.Reload();
        TownMenuRecruitmentLoader.Reload();
        TownMenuArmyLoader.Reload();
        RegionMenuBuildingSceneLoader.Reload();
    }

    public void RegisterArmy(OverworldArmy army)
    {
        _Armies.Add(army);
    }

    public void UnregisterArmy(OverworldArmy army)
    {
        _Armies.Remove(army);
    }
}
