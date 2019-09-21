using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public List<Quest> Quests = new List<Quest>();

    public void Add()
    {
        var quest = new Quest();
        quest.Objectives = new ObjectiveList();
        Quests.Add(quest);
    }

    public void Remove(int index) 
    {
        Quests.RemoveAt(index);
    }

    public int GetSize()
    {
        return Quests.Count;
    }
}

[System.Serializable]
public class Quest
{
    public string Title, Description;
    public ObjectiveList Objectives = new ObjectiveList();

}

[System.Serializable]
public struct Objective
{
    public ObjectiveType Type;
    public GameObject Target;
    public Vector2Int BuildingID;
    public string UnitName;
    public int UnitAmount;
}

public enum ObjectiveType
{
    BUILDING,
    UNIT,
    CONQUER,
    DEFEAT
};

[System.Serializable]
public class ObjectiveList
{
    public List<Objective> Objectives = new List<Objective>();

    public void Add()
    {
        Objectives.Add(new Objective());
    }

    public void Remove(int index)
    {
        Objectives.RemoveAt(index);
    }

    public int GetSize()
    {
        return Objectives.Count;
    }

}