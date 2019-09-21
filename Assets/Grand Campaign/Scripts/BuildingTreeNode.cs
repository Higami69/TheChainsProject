using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTreeNode {

    public Vector2Int Id;
    public BuildingTreeNode NextNode = null, PrevNode = null;
    public Building AttachedBuilding = null;
    private bool _CanBeActivated, _IsPurchased, _IsBuilding, _IsObsolete = false;
    public int NrPrerequisiteNodes = 1, BuildTime;
    private int _ActivePrerequisiteNodes = 0, _BuildTimeRemaining;
    public float Cost;
    public bool IsUnlocked = true;

    public void Activate()
    {
        if(++_ActivePrerequisiteNodes >= NrPrerequisiteNodes) _CanBeActivated = true;
    }

    public void Purchase()
    {
        if (MoneyManager.Instance.GetCurrentMoney() < Cost) return;

        _IsBuilding = true;
        _BuildTimeRemaining = BuildTime;
        MoneyManager.Instance.RemoveMoney(Cost);

        if (Player.Instance.SelectedTown != null) Player.Instance.SelectedTown.BuildingStarted(this);
        else Player.Instance.SelectedRegion.AddBuilding(this);
    }

    public void HandleTurnEnd(Region region, Town town)
    {
        if (_IsBuilding)
        {
            _BuildTimeRemaining--;
            if (_BuildTimeRemaining <= 0)
            {
                _IsBuilding = false;
                _IsPurchased = true;
                AttachedBuilding.HandleCreation(town, region);

                if (town != null) town.BuildingFinished(this);
                else region.FinishedBuilding(this);

                if (PrevNode != null) PrevNode.SetObsolete(town, region);
                if (NextNode != null) NextNode.Activate();
            }
        }

        if(_IsPurchased && AttachedBuilding && !_IsObsolete)
        {
            AttachedBuilding.HandleTurnEnd(town, region);
        }
    }

    public void SetObsolete(Town town, Region region)
    {
        _IsObsolete = true;
        AttachedBuilding.HandleObsoletion(town, region);
    }

    public bool IsActivated()
    {
        return _CanBeActivated;
    }

    public bool IsPurchased()
    {
        return _IsPurchased;
    }

    public bool IsBuilding()
    {
        return _IsBuilding;
    }

    public int GetBuildTimeRemaining()
    {
        return _BuildTimeRemaining;
    }

    public int GetBuildTime()
    {
        return BuildTime;
    }
}
