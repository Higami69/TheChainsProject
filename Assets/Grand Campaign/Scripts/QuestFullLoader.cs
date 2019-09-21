using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestFullLoader : MonoBehaviour {

    public Transform Background, Objectives;
    public GameObject ObjectiveTemplate;
    public float ObjectiveHeight = 40.0f;

    private void Awake()
    {
        Load();
    }
    public void Load()
    {
        var quest = ObjectiveManager.Instance.GetCurrentQuest();
        Background.Find("Title").GetComponent<Text>().text = quest.Title;
        Background.Find("Description").GetComponent<Text>().text = quest.Description;

        var objList = quest.Objectives.Objectives;
        float yPos = 0.0f;
        foreach(var objective in objList)
        {
            var obj = Instantiate(ObjectiveTemplate, Objectives);
            var pos = new Vector2(10, yPos);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            yPos -= ObjectiveHeight;

            switch(objective.Type)
            {
                case ObjectiveType.BUILDING:
                    if(objective.Target.GetComponent<Town>() != null)
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
        }
    }
}
