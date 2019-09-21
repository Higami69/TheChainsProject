using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Region : MonoBehaviour {

    public string Name;
    List<Town> _Towns;
    private BuildingTree _Tree;
    public float IncomeMultiplier = 1;
    public float MovementMultiplier = 1;
    private float _Income = 0;
    private int _NrBuildingsBuilt = 0;
    private List<BuildingTreeNode> _BuildQueue;

    void Start() {
        _BuildQueue = new List<BuildingTreeNode>();
        _Tree = GetComponent<BuildingTree>();
        _Towns = new List<Town>();
        foreach(var town in gameObject.GetComponentsInChildren<Town>())
        {
            _Towns.Add(town);
            town.SetRegion(this);
        }
	}
	
    public void HandleTurnEnd()
    {
        foreach(var town in _Towns)
        {
            town.HandleTurnEnd(this);
        }

        _Tree.HandleTurnEnd(this, null);
    }

    public void UpdateIncome()
    {
        float income = 0;

        foreach(var town in _Towns)
        {
            income += town.GetIncome() * IncomeMultiplier;
        }

        _Income = income;
    }

    public void OpenBuildingTree()
    {
        _Tree.PrepareForLoading();
        SceneManager.LoadScene("GC_BuildingTreeScene", LoadSceneMode.Additive);
    }

    public float GetIncome()
    {
        return _Income;
    }

    public void AddBuilding(BuildingTreeNode node)
    {
        _BuildQueue.Add(node);
        _NrBuildingsBuilt++;
    }

    public void FinishedBuilding(BuildingTreeNode node)
    {
        _BuildQueue.Remove(node);
        _NrBuildingsBuilt--;
    }

    public List<BuildingTreeNode> GetBuildQueue()
    {
        var queue = new List<BuildingTreeNode>();
        int idx = 1;
        int remaining = _BuildQueue.Count;
        while (remaining > 0)
        {
            foreach (var node in _BuildQueue)
            {
                if (node.GetBuildTimeRemaining() == idx)
                {
                    queue.Add(node);
                    remaining--;
                }
            }
            ++idx;
        }

        return queue;
    }

    public int GetBuildingsBeingBuilt()
    {
        return _NrBuildingsBuilt;
    }

    public bool IsBuildingBuilt(Vector2Int id)
    {
        return _Tree.IsBuildingBuilt(id);
    }

    public string GetBuildingName(Vector2Int id)
    {
        foreach (var node in _Tree.EditorTree)
        {
            if (node.Id == id)
                return node.AttachedBuilding.GetName();
        }

        return null;
    }

    public bool IsUnlocked()
    {
        foreach(var town in _Towns)
        {
            var unlockable = town.gameObject.GetComponent<Unlockable>();
            if (unlockable != null && !unlockable.IsUnlocked()) return false; 
        }

        return true;
    }

    public Owner GetOwner()
    {
        Owner owner = _Towns[0].transform.GetComponent<Ownership>().Owner;
        for(int i = 1; i < _Towns.Count; i++)
        {
            if (owner != _Towns[i].transform.GetComponent<Ownership>().Owner)
                return Owner.CONTESTED;
        }

        return owner;
    }

    public bool IsPointInRegion(Vector3 point)
    {
        var area = GetComponent<SelectableArea>()._Area;
        float xMin = 99999.0f; float zMin = 99999.0f;
        float xMax = -99999.0f; float zMax = -99999.0f;

        foreach(Vector3 p in area)
        {
            if (p.x < xMin) xMin = p.x;
            if (p.x > xMax) xMax = p.x;
            if (p.z < zMin) zMin = p.z;
            if (p.z > zMax) zMax = p.z;
        }

        if(point.x >= xMin && point.x <= xMax && point.z >= zMin && point.z <= zMax)
        {
            return true;
        }
        else return false;
    }
}
