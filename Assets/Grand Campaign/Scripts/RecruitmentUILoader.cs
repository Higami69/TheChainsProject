using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentUILoader : MonoBehaviour {

    public GameObject Template;
    public Transform Content;
    public float XOffset = 40.0f;
    public float BlockWidth = 300.0f;

    private const int _MaxFitX = 3;
    private const float _YPos = -20.0f;

    void Start()
    {
        if (UnitListToLoad.Instance.Count > _MaxFitX)
        {
            var offset = (XOffset + BlockWidth) * (UnitListToLoad.Instance.Count - _MaxFitX);
            Content.GetComponent<RectTransform>().offsetMax = new Vector2(-offset, 0);
        }

        int idx = 0;
        foreach(var data in UnitListToLoad.Instance)
        {
            var obj = GameObject.Instantiate(Template, Content);
            var pos = new Vector3();
            pos.x = (XOffset / 2.0f) + idx * (BlockWidth + XOffset);
            pos.y = _YPos;
            obj.GetComponent<RectTransform>().anchoredPosition = pos;

            obj.transform.Find("Name").GetComponent<Text>().text = data.Name;
            obj.transform.Find("Description").GetComponent<Text>().text = data.Description;
            obj.transform.Find("HealthValue").GetComponent<Text>().text =
                data.Stats.HealthMin.ToString() + " - " + data.Stats.HealthMax.ToString();
            obj.transform.Find("SpeedValue").GetComponent<Text>().text = 
                data.Stats.SpeedMin.ToString() + " - " + data.Stats.SpeedMax.ToString();
            obj.transform.Find("MoraleValue").GetComponent<Text>().text =
                data.Stats.MoraleMin.ToString() + " - " + data.Stats.MoraleMax.ToString();
            obj.transform.Find("RangeValue").GetComponent<Text>().text =
                data.Stats.RangeMin.ToString() + " - " + data.Stats.RangeMax.ToString();
            obj.transform.Find("MeleeAtkValue").GetComponent<Text>().text =
                data.Stats.Melee_AttackMin.ToString() + " - " + data.Stats.Melee_AttackMax.ToString();
            obj.transform.Find("MeleeDefValue").GetComponent<Text>().text =
                data.Stats.Melee_DefenseMin.ToString() + " - " + data.Stats.Melee_DefenseMax.ToString();
            obj.transform.Find("RangedAtkValue").GetComponent<Text>().text =
                data.Stats.Ranged_AttackMin.ToString() + " - " + data.Stats.Ranged_AttackMax.ToString();
            obj.transform.Find("RangedDefValue").GetComponent<Text>().text =
                data.Stats.Ranged_DefenseMin.ToString() + " - " + data.Stats.Ranged_DefenseMax.ToString();

            obj.transform.Find("RecruitButton").GetComponent<RecruitmentUIButton>().data = data;
            obj.transform.Find("RecruitButton").Find("Cost").GetComponent<Text>().text = data.Cost.ToString() + " ";

            idx++;
        }
    }
	
}

public class UnitListToLoad
{
    private static List<UnitData> _Instance;
    public static List<UnitData> Instance
    {
        get
        {
            return _Instance;
        }
        set
        {
            _Instance = value;
        }
    }
}
