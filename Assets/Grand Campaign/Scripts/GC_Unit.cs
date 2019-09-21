using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitStats
{
    public float Health;
    public float Speed;
    public float Morale;
    public float Range;
    public float Melee_Attack;
    public float Melee_Defense;
    public float Ranged_Attack;
    public float Ranged_Defense;
}

[System.Serializable]
public struct UnitStatBounds //Used to generate stats
{
    public float HealthMin, HealthMax;
    public float SpeedMin, SpeedMax;
    public float MoraleMin, MoraleMax;
    public float RangeMin, RangeMax;
    public float Melee_AttackMin, Melee_AttackMax;
    public float Melee_DefenseMin, Melee_DefenseMax;
    public float Ranged_AttackMin, Ranged_AttackMax;
    public float Ranged_DefenseMin, Ranged_DefenseMax;
}

public class GC_Unit {
    public string Name;
    public GC_Division Division;
    private UnitStats _Stats;

    public UnitStats GetStats()
    {
        return _Stats;
    }
    public void GenerateStats(UnitStatBounds bounds)
    {
        _Stats = new UnitStats();
        _Stats.Health = (int)Random.Range(bounds.HealthMin, bounds.HealthMax);
        _Stats.Speed = (int)Random.Range(bounds.SpeedMin, bounds.SpeedMax);
        _Stats.Morale = (int)Random.Range(bounds.MoraleMin, bounds.MoraleMax);
        _Stats.Range = (int)Random.Range(bounds.RangeMin, bounds.RangeMax);
        _Stats.Melee_Attack = (int)Random.Range(bounds.Melee_AttackMin, bounds.Melee_AttackMax);
        _Stats.Melee_Defense = (int)Random.Range(bounds.Melee_DefenseMin, bounds.Melee_DefenseMax);
        _Stats.Ranged_Attack = (int)Random.Range(bounds.Ranged_AttackMin, bounds.Ranged_AttackMax);
        _Stats.Ranged_Defense = (int)Random.Range(bounds.Ranged_DefenseMin, bounds.Ranged_DefenseMax);
    }
}
