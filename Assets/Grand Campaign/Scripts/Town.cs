using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Town : MonoBehaviour {

    public string Name;
    public int MaxRecruits = 2;
    private BuildingTree _BuildingTree;
    private float _Income = 0;
    private int _BuildingsBeingBuilt = 0;
    private List<BuildingTreeNode> _BuildQueue;
    private GC_Army _Garrison;
    private List<UnitData> _TrainableUnits;
    private Region _Region;

    private Queue<GC_Unit> _RecruitmentQueue;
    private List<int> _RecruitsPerTurn;

	void Awake() {
        _BuildingTree = GetComponent<BuildingTree>();
        _BuildQueue = new List<BuildingTreeNode>();
        _Garrison = new GC_Army();
        _Garrison.Position = transform.position;
        _TrainableUnits = new List<UnitData>();
        _RecruitmentQueue = new Queue<GC_Unit>();
        _RecruitsPerTurn = new List<int>();
    }
	
    public void SetRegion(Region region)
    {
        _Region = region;
    }

	void Update () {
		//Check owner
        if(GetComponent<Ownership>().Owner == Owner.PLAYER)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void OpenBuildingTree()
    {
        _BuildingTree.PrepareForLoading();
        SceneManager.LoadScene("GC_BuildingTreeScene", LoadSceneMode.Additive);
    }

    public void HandleTurnEnd(Region region)
    {
        _BuildingTree.HandleTurnEnd(region, this);

        if (_RecruitmentQueue.Count > 0)
        {
            var newDivisions = new Dictionary<string, GC_Division>();
            int nrRecruitsDone = _RecruitsPerTurn[0];
            for(int i = 0; i < nrRecruitsDone; i++)
            {
                var unit = _RecruitmentQueue.Dequeue();
                Player.Instance.AddUnit(unit.Name);

                if (!newDivisions.ContainsKey(unit.Name)) newDivisions[unit.Name] = new GC_Division();
                newDivisions[unit.Name].AddUnit(unit);
            }
            foreach(var div in newDivisions)
            {
                _Garrison.AddDivision(div.Value);
            }
            for(int i = 1; i < _RecruitsPerTurn.Count; i++)
            {
                _RecruitsPerTurn[i - 1] = _RecruitsPerTurn[i];
            }
            _RecruitsPerTurn.RemoveAt(_RecruitsPerTurn.Count - 1);
        }


    }

    public void ChangeIncome(float amount)
    {
        _Income += amount;
    }

    public float GetIncome()
    {
        return _Income;
    }

    public void BuildingStarted(BuildingTreeNode node)
    {
        _BuildingsBeingBuilt++;
        _BuildQueue.Add(node);
    }

    public void BuildingFinished(BuildingTreeNode node)
    {
        _BuildingsBeingBuilt--;
        _BuildQueue.Remove(node);
    }

    public int GetBuildingsBeingBuilt()
    {
        return _BuildingsBeingBuilt;
    }

    public List<BuildingTreeNode> GetBuildQueue()
    {
        var queue = new List<BuildingTreeNode>();
        int idx = 1;
        int remaining = _BuildQueue.Count;
        while(remaining > 0)
        {
            foreach(var node in _BuildQueue)
            {
                if(node.GetBuildTimeRemaining() == idx)
                {
                    queue.Add(node);
                    remaining--;
                }
            }
            ++idx;
        }

        return queue;
    }

    public GC_Army GetGarrison()
    {
        return _Garrison;
    }

    public void EnableUnitForTraining(UnitData unit)
    {
        _TrainableUnits.Add(unit);
    }

    public List<UnitData> GetTrainableList()
    {
        return _TrainableUnits;
    }

    public void Recruit(UnitData unitData)
    {
        var unit = new GC_Unit();
        unit.Name = unitData.Name;
        unit.GenerateStats(unitData.Stats);

        _RecruitmentQueue.Enqueue(unit);
        if(_RecruitsPerTurn.Count == 0 || _RecruitsPerTurn[_RecruitsPerTurn.Count - 1] == MaxRecruits)
        {
            _RecruitsPerTurn.Add(0);
        }
        _RecruitsPerTurn[_RecruitsPerTurn.Count - 1]++;
    }

    public Queue<GC_Unit> GetRecruitmentQueue()
    {
        return _RecruitmentQueue;
    }

    public List<int> GetRecruitTimes()
    {
        return _RecruitsPerTurn;
    }

    public Region GetRegion()
    {
        return _Region;
    }

    public string GetBuildingName(Vector2Int id)
    {
        foreach(var node in _BuildingTree.EditorTree)
        {
            if (node.Id == id)
                return node.AttachedBuilding.GetName();
        }

        return null;
    }

    public bool IsBuildingBuilt(Vector2Int id)
    {
        return _BuildingTree.IsBuildingBuilt(id);
    }

    public void ClearGarrison()
    {
        _Garrison = new GC_Army();
        _Garrison.Position = transform.position;
        TownMenuArmyLoader.Reload();
    }
}
