using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmyLoader : MonoBehaviour {
    public UnitList Units;
    private Dictionary<string, int> _Units;

    private void Awake()
    {
        _Units = new Dictionary<string, int>();
        //Load it into a proper dictionary for ease of use
        for(int i = 0; i < Units.Keys.Count; i++)
        {
            _Units.Add(Units.Keys[i], Units.Values[i]);
        }
    }

    private void Start()
    {
        GC_Army army = new GC_Army();

        if (gameObject.CompareTag("Town"))
        {
            var town = GetComponent<Town>();
            army = town.GetGarrison();
        }
        else if (gameObject.CompareTag("Army"))
        {
            if (GetComponent<OverworldArmy>().Army == null)
                GetComponent<OverworldArmy>().Army = new GC_Army();

            army = GetComponent<OverworldArmy>().Army;
        }

        foreach (var data in _Units)
        {
            var div = new GC_Division();
            for (int i = 0; i < data.Value; i++)
            {
                var unit = new GC_Unit();
                unit.Name = data.Key;
                unit.GenerateStats(UnitDatabase.Instance.GetUnit(data.Key).Stats);
                div.AddUnit(unit);
            }
            army.AddDivision(div);
        }
    }
}