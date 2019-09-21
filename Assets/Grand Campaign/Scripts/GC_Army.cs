using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_Army {
    private List<GC_Division> _Divisions;
    private GC_Unit _General;
    private OverworldArmy _OverworldArmy;

    private const float _MoveRange = 5.0f;
    public Vector3 Position;
    public bool InOverWorld = false;

    private void Start()
    {
        _Divisions = new List<GC_Division>();
    }
    public void AddDivision(GC_Division division)
    {
        if (_Divisions == null) _Divisions = new List<GC_Division>();
        _Divisions.Add(division);
        division.SetArmy(this);
    }

    public GC_Unit GetGeneral()
    {
        return _General;
    }

    public void SetGeneral(GC_Unit general)
    {
        _General = general;
    }

    public List<GC_Division> GetDivisions()
    {
        return _Divisions;
    }

    public void ReloadDivisions()
    {
        var list = new List<GC_Division>();
        foreach(var div in _Divisions)
        {
            if (div.GetCaptain() == null) list.Add(div);
        }
        foreach(var div in list)
        {
            _Divisions.Remove(div);
        }
    }

    public int GetSize()
    {
        int amount = 0;
        if (_General != null) amount++;
        if (_Divisions != null)
        {
            foreach (var div in _Divisions)
            {
                if (div.GetCaptain() != null) amount++;
                amount += div.GetUnits().Count;
            }
        }

        return amount;
    }

    public float GetMoveRange()
    {
        foreach(var region in GameObject.FindGameObjectsWithTag("Region"))
        {
            if (region.GetComponent<Region>().IsPointInRegion(Position))
                return _MoveRange * region.GetComponent<Region>().MovementMultiplier;
        }
        return _MoveRange;
    }

    public void DisplayRange()
    {
        ArmyRangeDisplay.Instance.SetPosition(Position);
        ArmyRangeDisplay.Instance.SetSize(GetMoveRange());
    }

    public void SetOverworldArmy(OverworldArmy army)
    {
        _OverworldArmy = army;
    }

    public OverworldArmy GetOverworldArmy()
    {
        return _OverworldArmy;
    }

    public void Unregister()
    {
        var player = Player.Instance;

        if(_General != null)
        {
            player.RemoveUnit(_General.Name);
        }

        foreach(var div in _Divisions)
        {
            if(div.GetCaptain() != null)
            {
                player.RemoveUnit(div.GetCaptain().Name);
            }

            foreach(var unit in div.GetUnits())
            {
                player.RemoveUnit(unit.Name);
            }
        }
    }
}
