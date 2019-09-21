using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSceneLoader : MonoBehaviour {
    public Transform Background, Objectives;
    public GameObject ObjectiveTemplate;
    public float ObjectiveHeight = 40.0f;

    private static QuestSceneLoader _Instance;
    public static QuestSceneLoader Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        _Instance = this;
        Load();
    }

    private void Update()
    {
        var objectives = ObjectiveManager.Instance.GetCurrentQuest().Objectives.Objectives;
        for(int i = 0; i < objectives.Count; i++)
        {
            GameObject obj = null;
            for(int j = 0; j < Objectives.childCount; j++)
            {
                if (Objectives.GetChild(j).gameObject.GetComponent<ObjectiveSmallID>().ID == i)
                {
                    obj = Objectives.GetChild(j).gameObject;
                    break;
                }
            }

            if (obj != null)
            {
                obj.transform.Find("CurrAmount").GetComponent<Text>().text = ObjectiveManager.Instance.GetObjectiveTrackerAmount(i).ToString();
            }
        }
    }

    public void Load()
    {
        Background.Find("Title").GetComponent<Text>().text = ObjectiveManager.Instance.GetCurrentQuest().Title;
        for(int i = 0; i < Objectives.childCount; i++)
        {
            Destroy(Objectives.GetChild(i).gameObject);
        }

        var objectives = ObjectiveManager.Instance.GetCurrentQuest().Objectives.Objectives;
        float yPos = 0.0f;
        int id = 0;
        foreach(var objective in objectives)
        {
            var obj = Instantiate(ObjectiveTemplate, Objectives);
            var pos = new Vector2(0, yPos);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            obj.GetComponent<ObjectiveSmallID>().ID = id;
            yPos -= ObjectiveHeight;

            switch (objective.Type)
            {
                case ObjectiveType.BUILDING:
                    if (objective.Target.GetComponent<Town>() != null)
                        obj.GetComponent<Text>().text = objective.Target.GetComponent<Town>().Name + ": "
                            + objective.Target.GetComponent<Town>().GetBuildingName(objective.BuildingID);
                    else obj.GetComponent<Text>().text = objective.Target.GetComponent<Region>().Name + ": "
                            + objective.Target.GetComponent<Region>().GetBuildingName(objective.BuildingID);
                    obj.transform.Find("Amount").GetComponent<Text>().text = "/ 1 ";
                    break;
                case ObjectiveType.CONQUER:
                    obj.GetComponent<Text>().text = objective.Target.GetComponent<Town>().Name;
                    obj.transform.Find("Amount").GetComponent<Text>().text = "/ 1 ";
                    break;
                case ObjectiveType.UNIT:
                    obj.GetComponent<Text>().text = objective.UnitName + ":";
                    obj.transform.Find("Amount").GetComponent<Text>().text = "/ " + objective.UnitAmount.ToString() + " ";
                    break;
                case ObjectiveType.DEFEAT:
                    obj.GetComponent<Text>().text = "Army at " + objective.Target.GetComponent<OverworldArmy>().GetRegion().Name;
                    obj.transform.Find("Amount").GetComponent<Text>().text = "/ 1 ";
                    break;
            }
            id++;
        }
    }
}
