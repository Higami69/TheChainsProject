using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTree : MonoBehaviour
{
    public List<NodeData> EditorTree;
    private List<BuildingTreeNode> _Tree;

    public void PrepareForLoading()
    {
        BuildingTreeToLoad.Instance = _Tree;
    }

    private void Start()
    {
        //Convert EditorTree to Tree
        var placedNodes = new Dictionary<Vector2Int, BuildingTreeNode>();
        _Tree = new List<BuildingTreeNode>();

        foreach (var data in EditorTree)
        {
            var node = new BuildingTreeNode();

            node.Id = data.Id;
            node.NrPrerequisiteNodes = data.NrRequiredNodes;
            node.Cost = data.Cost;
            node.BuildTime = data.BuildTime;
            node.AttachedBuilding = data.AttachedBuilding;
            if (data.IsActive) node.Activate();

            if(data.QuestIdx >= 0)
            {
                node.IsUnlocked = false;
                ObjectiveManager.Instance.Register(node, data.QuestIdx);
            }

            placedNodes.Add(node.Id, node);
            _Tree.Add(node);
        }

        foreach(var data in EditorTree)
        {
            if (data.UpgradesFrom != new Vector2Int(0, 0)) placedNodes[data.Id].PrevNode = placedNodes[data.UpgradesFrom];
            if (data.NextNode == new Vector2Int(0, 0)) continue;

            var node = placedNodes[data.Id];
            node.NextNode = placedNodes[data.NextNode];
        }
    }

    public void HandleTurnEnd(Region region, Town town)
    {
        foreach(var node in _Tree)
        {
            node.HandleTurnEnd(region, town);
        }
    }

    public bool IsBuildingBuilt(Vector2Int id)
    {
        foreach(var node in _Tree)
        {
            if (node.Id == id)
                return node.IsPurchased();
        }

        return false;
    }
}

[System.Serializable]
public struct NodeData
{
    public Vector2Int Id;
    public Vector2Int NextNode;
    public Vector2Int UpgradesFrom;
    public Building AttachedBuilding;
    public int NrRequiredNodes, BuildTime;
    public float Cost;
    public bool IsActive;
    public int QuestIdx;
}

public class BuildingTreeToLoad : MonoBehaviour
{
    private static List<BuildingTreeNode> _instance;
    public static List<BuildingTreeNode> Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
}
