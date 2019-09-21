using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMenuRecruitmentLoader : MonoBehaviour {
    public GameObject Template;
    public Transform Background;
    public float Offset, NodeWidth, NodeHeight;
    public int AmountY = 2;

    private static TownMenuRecruitmentLoader _Instance;

    public static void Reload()
    {
        if(_Instance != null) _Instance.Load();
    }

    public void Load()
    {
        for (int i = 0; i < Background.childCount; i++)
        {
            Destroy(Background.GetChild(i).gameObject);
        }

        var queue = new Queue<GC_Unit>(Player.Instance.SelectedTown.GetRecruitmentQueue());
        var recruitTimes = new List<int>(Player.Instance.SelectedTown.GetRecruitTimes());

        int x = 0, y = 0;

        for(int i = 0; i < recruitTimes.Count; i++)
        {
            Dictionary<string, int> unitsPerType = new Dictionary<string, int>();
            for(int j = 0; j < recruitTimes[i]; j++)
            {
                var unit = queue.Dequeue();
                if (!unitsPerType.ContainsKey(unit.Name)) unitsPerType[unit.Name] = 0;
                unitsPerType[unit.Name]++; 
            }

            foreach(var data in unitsPerType)
            {
                var obj = Instantiate(Template, Background);

                var pos = new Vector3();
                pos.x = (Offset / 2) + x * (Offset + NodeWidth);
                pos.y = -((Offset / 2) + y * (Offset + NodeHeight));
                obj.GetComponent<RectTransform>().anchoredPosition = pos;

                obj.transform.Find("Name").GetComponent<Text>().text = data.Key;
                obj.transform.Find("Time").GetComponent<Text>().text = (i + 1).ToString();
                obj.transform.Find("Amount").GetComponent<Text>().text = data.Value.ToString();

                y++;
                if(y == AmountY)
                {
                    y = 0;
                    x++;
                }
            }
        }
    }

    private void Start()
    {
        if (_Instance == null) _Instance = this;
        Load();
    }
}
