using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour {
    private Dictionary<int, UnlockData> _UnlockData;
    public QuestList Quests;
    private int _CurrentQuest = 0;
    private bool _HasDisplayedQuest = false;
    private List<int> _ObjectiveTracker = new List<int>();
    private List<bool> _ObjectiveChecker = new List<bool>();

    private static ObjectiveManager _Instance;
    public static ObjectiveManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        _UnlockData = new Dictionary<int, UnlockData>();
        _Instance = this;
    }

    private void Start()
    {
        //Make sure tracker and checker are the correct size
        var objectives = Quests.Quests[_CurrentQuest].Objectives.Objectives;
        if (_ObjectiveTracker.Count != objectives.Count)
        {
            while (_ObjectiveTracker.Count < objectives.Count)
            {
                _ObjectiveTracker.Add(0);
            }
            while (_ObjectiveTracker.Count > objectives.Count)
            {
                _ObjectiveTracker.RemoveAt(_ObjectiveTracker.Count - 1);
            }
        }
        if (_ObjectiveChecker.Count != objectives.Count)
        {
            while (_ObjectiveChecker.Count < objectives.Count)
            {
                _ObjectiveChecker.Add(false);
            }
            while (_ObjectiveChecker.Count > objectives.Count)
            {
                _ObjectiveChecker.RemoveAt(_ObjectiveChecker.Count - 1);
            }
        }
    }

    private void Update()
    {
        if (_CurrentQuest < Quests.Quests.Count)
        {
            if (!_HasDisplayedQuest) DisplayQuestFull();

            TrackQuest();
            CheckQuest();
            if (CheckIfQuestDone()) Unlock();
        }
    }

    public void Register(Unlockable unlockable, int questIdx)
    {
        if(_UnlockData.ContainsKey(questIdx))
        {
            _UnlockData[questIdx].Unlockables.Add(unlockable);
        }
        else
        {
            _UnlockData.Add(questIdx, new UnlockData());
            Register(unlockable, questIdx);
        }
    }

    public void Register(BuildingTreeNode node, int questIdx)
    {
        if (_UnlockData.ContainsKey(questIdx))
        {
            _UnlockData[questIdx].Nodes.Add(node);
        }
        else
        {
            _UnlockData.Add(questIdx, new UnlockData());
            Register(node, questIdx);
        }
    }

    private void Unlock()
    {
        if (_UnlockData.ContainsKey(_CurrentQuest))
        {
            foreach (var unlockable in _UnlockData[_CurrentQuest].Unlockables)
            {
                unlockable.Unlock();
            }
            foreach (var node in _UnlockData[_CurrentQuest].Nodes)
            {
                node.IsUnlocked = true;
            }
        }

        if (_CurrentQuest < Quests.Quests.Count - 1)
        {
            ++_CurrentQuest;
            _HasDisplayedQuest = false;
            QuestSceneLoader.Instance.Load();

            for (int i = 0; i < _ObjectiveTracker.Count; i++)
            {
                _ObjectiveTracker[i] = 0;
                _ObjectiveChecker[i] = false;
            }

            var objectives = Quests.Quests[_CurrentQuest].Objectives.Objectives;
            if (_ObjectiveTracker.Count != objectives.Count)
            {
                while (_ObjectiveTracker.Count < objectives.Count)
                {
                    _ObjectiveTracker.Add(0);
                }
                while (_ObjectiveTracker.Count > objectives.Count)
                {
                    _ObjectiveTracker.RemoveAt(_ObjectiveTracker.Count - 1);
                }
            }
            if (_ObjectiveChecker.Count != objectives.Count)
            {
                while (_ObjectiveChecker.Count < objectives.Count)
                {
                    _ObjectiveChecker.Add(false);
                }
                while (_ObjectiveChecker.Count > objectives.Count)
                {
                    _ObjectiveChecker.RemoveAt(_ObjectiveChecker.Count - 1);
                }
            }
        }
        else
        {
            SceneManager.LoadScene("GC_VictoryScene", LoadSceneMode.Single);
        }
    }

    public Quest GetCurrentQuest()
    {
        if (_CurrentQuest < Quests.Quests.Count)
            return Quests.Quests[_CurrentQuest];
        else return Quests.Quests[_CurrentQuest - 1];
    }

    public void DisplayQuestFull()
    {
        _HasDisplayedQuest = true;
        if(QuestSceneLoader.Instance != null) SceneManager.UnloadSceneAsync("GC_QuestScene");
        SceneManager.LoadSceneAsync("GC_QuestFullScene", LoadSceneMode.Additive);
        Player.Instance.SetTreeOpen(true);
    }

    private void TrackQuest()
    {
        var objectives = Quests.Quests[_CurrentQuest].Objectives.Objectives;
            for(int i = 0; i < objectives.Count; i++)
            {
                switch (objectives[i].Type)
                {
                    case ObjectiveType.BUILDING:
                    if (objectives[i].Target.GetComponent<Town>() != null)
                    {
                        if (objectives[i].Target.GetComponent<Town>().IsBuildingBuilt(objectives[i].BuildingID)) _ObjectiveTracker[i] = 1;
                        else _ObjectiveTracker[i] = 0;
                    }
                    else
                    {
                        if (objectives[i].Target.GetComponent<Region>().IsBuildingBuilt(objectives[i].BuildingID)) _ObjectiveTracker[i] = 1;
                        else _ObjectiveTracker[i] = 0;
                    }
                        break;
                    case ObjectiveType.CONQUER:
                    if (objectives[i].Target.GetComponent<Ownership>().Owner == Owner.PLAYER)
                    {
                        _ObjectiveTracker[i] = 1;
                    }
                    else _ObjectiveTracker[i] = 0;
                        break;
                    case ObjectiveType.UNIT:
                    _ObjectiveTracker[i] = Player.Instance.GetUnitAmount(objectives[i].UnitName); 
                        break;
                case ObjectiveType.DEFEAT:
                    if (objectives[i].Target == null) _ObjectiveTracker[i] = 1;
                    else _ObjectiveTracker[i] = 0;
                    break;
                }
            }
    }
    private void CheckQuest()
    {
        var objectives = Quests.Quests[_CurrentQuest].Objectives.Objectives;
        for(int i = 0; i < objectives.Count; i++)
        {
            switch(objectives[i].Type)
            {
                case ObjectiveType.BUILDING:
                case ObjectiveType.CONQUER:
                case ObjectiveType.DEFEAT:
                    if (_ObjectiveTracker[i] == 1) _ObjectiveChecker[i] = true;
                    break;
                case ObjectiveType.UNIT:
                    _ObjectiveChecker[i] = _ObjectiveTracker[i] >= objectives[i].UnitAmount;
                    break;
            }
        }
    }
    private bool CheckIfQuestDone()
    {
        foreach(bool b in _ObjectiveChecker)
        {
            if (!b) return false;
        }

        return true;
    }

    public int GetObjectiveTrackerAmount(int i)
    {
        return _ObjectiveTracker[i];
    }
}

public class UnlockData
{
    public UnlockData()
    {
        Unlockables = new List<Unlockable>();
        Nodes = new List<BuildingTreeNode>();
    }

    public List<Unlockable> Unlockables;
    public List<BuildingTreeNode> Nodes;
}

