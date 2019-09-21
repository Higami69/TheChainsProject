using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_Division {
    private List<GC_Unit> _Units;
    private GC_Unit _Captain;
    private GC_Army _Army;

    private void Start()
    {
    }
    public void AddUnit(GC_Unit unit)
    {
        if (_Units == null) _Units = new List<GC_Unit>();
        if (_Captain != null) _Units.Add(unit);
        else _Captain = unit;

        unit.Division = this;
    }

    public void SetCaptain(GC_Unit unit)
    {
        _Units.Add(_Captain);
        _Captain = unit;
        _Units.Remove(unit);
    }

    public void RemoveUnit(GC_Unit unit)
    {
        _Units.Remove(unit);
        unit.Division = null;
    }

    public GC_Unit GetCaptain()
    {
        return _Captain;
    }

    public List<GC_Unit> GetUnits()
    {
        return _Units;
    }

    public void SetArmy(GC_Army army)
    {
        _Army = army;
    }

    public void RemoveCaptain()
    {
        if(_Units.Count > 0)
        {
            _Captain = _Units[0];
            _Units.RemoveAt(0);
            return;
        }
        _Captain = null;
        _Army.ReloadDivisions();
    }
}
