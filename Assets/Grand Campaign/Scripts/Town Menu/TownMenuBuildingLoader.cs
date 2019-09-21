using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMenuBuildingLoader : MonoBehaviour {
    public GameObject NodeTemplate;
    public Transform Background;
    public float Offset, NodeWidth, NodeHeight;
    public int AmountY = 2;

    private static TownMenuBuildingLoader _Instance;

    public static void Reload()
    {
        if(_Instance != null) _Instance.Load();
    }
    public void Load()
    {
        for(int i = 0; i < Background.childCount; i++)
        {
            Destroy(Background.GetChild(i).gameObject);
        }

        var queue = Player.Instance.SelectedTown.GetBuildQueue();
        int x = 0; int y = 0;

        foreach(var node in queue)
        {
            var obj = Instantiate(NodeTemplate, Background);
            var pos = new Vector3();
            pos.x = (Offset / 2) + x * (Offset + NodeWidth);
            pos.y = -((Offset / 2) + y * (Offset + NodeHeight));
            obj.GetComponent<RectTransform>().anchoredPosition = pos;

            obj.transform.Find("Name").GetComponent<Text>().text = node.AttachedBuilding.GetName();
            obj.transform.Find("Time").GetComponent<Text>().text = node.GetBuildTimeRemaining().ToString();

            y++;
            if(y == AmountY)
            {
                y = 0;
                x++;
            }
        }
    }

    private void Start()
    {
        _Instance = this;
        Load();
    }

    private void OnDestroy()
    {
        _Instance = null;
    }
}
