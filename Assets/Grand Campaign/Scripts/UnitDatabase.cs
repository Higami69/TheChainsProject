using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour {
    private static UnitDatabase _Instance;
    public static UnitDatabase Instance
    {
        get
        {
            return _Instance;
        }
    }

    private Dictionary<string, UnitData> _Database = new Dictionary<string, UnitData>();

    private void Awake()
    {
        _Instance = this;
        RegisterUnits();
    }

    private void RegisterUnits()
    {
        //Spearman
        var unit = new UnitData();
        unit.Name = "Spearman";
        unit.Description = "Basic defensive unit";
        unit.Cost = 5.0f;
        unit.Stats.HealthMin = 50.0f;
        unit.Stats.HealthMax = 70.0f;
        unit.Stats.SpeedMin = 3.0f;
        unit.Stats.SpeedMax = 5.0f;
        unit.Stats.RangeMin = 1.0f;
        unit.Stats.RangeMax = 1.0f;
        unit.Stats.MoraleMin = 20.0f;
        unit.Stats.MoraleMax = 50.0f;
        unit.Stats.Melee_AttackMin = 2.0f;
        unit.Stats.Melee_AttackMax = 3.0f;
        unit.Stats.Melee_DefenseMin = 5.0f;
        unit.Stats.Melee_DefenseMax = 8.0f;
        unit.Stats.Ranged_AttackMin = 0.0f;
        unit.Stats.Ranged_AttackMax = 0.0f;
        unit.Stats.Ranged_DefenseMin = 1.0f;
        unit.Stats.Ranged_DefenseMax = 3.0f;

        _Database.Add(unit.Name, unit);

        //Brute
        unit = new UnitData();
        unit.Name = "Brute";
        unit.Description = "Basic offensive unit";
        unit.Cost = 10.0f;
        unit.Stats.HealthMin = 30.0f;
        unit.Stats.HealthMax = 50.0f;
        unit.Stats.SpeedMin = 3.0f;
        unit.Stats.SpeedMax = 6.0f;
        unit.Stats.RangeMin = 1.0f;
        unit.Stats.RangeMax = 1.0f;
        unit.Stats.MoraleMin = 30.0f;
        unit.Stats.MoraleMax = 60.0f;
        unit.Stats.Melee_AttackMin = 4.0f;
        unit.Stats.Melee_AttackMax = 6.0f;
        unit.Stats.Melee_DefenseMin = 2.0f;
        unit.Stats.Melee_DefenseMax = 4.0f;
        unit.Stats.Ranged_AttackMin = 0.0f;
        unit.Stats.Ranged_AttackMax = 0.0f;
        unit.Stats.Ranged_DefenseMin = 1.0f;
        unit.Stats.Ranged_DefenseMax = 2.0f;

        _Database.Add(unit.Name, unit);

        //Slinger
        unit = new UnitData();
        unit.Name = "Slinger";
        unit.Description = "Basic ranged unit";
        unit.Cost = 20.0f;
        unit.Stats.HealthMin = 20.0f;
        unit.Stats.HealthMax = 30.0f;
        unit.Stats.SpeedMin = 2.0f;
        unit.Stats.SpeedMax = 5.0f;
        unit.Stats.RangeMin = 3.0f;
        unit.Stats.RangeMax = 7.0f;
        unit.Stats.MoraleMin = 10.0f;
        unit.Stats.MoraleMax = 30.0f;
        unit.Stats.Melee_AttackMin = 1.0f;
        unit.Stats.Melee_AttackMax = 1.0f;
        unit.Stats.Melee_DefenseMin = 1.0f;
        unit.Stats.Melee_DefenseMax = 2.0f;
        unit.Stats.Ranged_AttackMin = 2.0f;
        unit.Stats.Ranged_AttackMax = 5.0f;
        unit.Stats.Ranged_DefenseMin = 1.0f;
        unit.Stats.Ranged_DefenseMax = 2.0f;

        _Database.Add(unit.Name, unit);

    }

    public UnitData GetUnit(string name)
    {
        return _Database[name];
    }
}
