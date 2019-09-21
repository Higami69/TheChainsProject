using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMenuArmyLoader : MonoBehaviour {
    public GameObject DivisionTemplate, DivUnitTemplate;
    public GameObject GeneralNode;
    public Transform Background, Content;
    public float Offset, NodeWidth, StartX, YPos, DivUnitOffset;

    private const int MaxDivisionsVisible = 5;
    private const float DivUnitStartY = -55.0f;

    private static TownMenuArmyLoader _Instance;
    private void Load()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Destroy(Content.GetChild(i).gameObject);
        }

        int nrDivisions = 0;
        int nrUnits = 0;

        GC_Army army = null;
        if (Player.Instance.SelectedTown != null)
        {
            army = Player.Instance.SelectedTown.GetGarrison();
        }
        else if(Player.Instance.SelectedArmy != null)
        {
            army = Player.Instance.SelectedArmy;
        }

        if (army != null)
        {
            if (army.GetGeneral() != null)
            {
                GeneralNode.transform.Find("Name").GetComponent<Text>().text = army.GetGeneral().Name;
                nrUnits++;
            }

            var divisions = army.GetDivisions();

            if (divisions == null) return;

            int x = 0;
            foreach (var div in divisions)
            {
                nrDivisions++;

                var node = Instantiate(DivisionTemplate, Content);
                var pos = new Vector3();
                pos.x = StartX + (x * (Offset + NodeWidth));
                pos.y = YPos;
                node.GetComponent<RectTransform>().anchoredPosition = pos;
                node.GetComponent<TownMenuArmyDivision>().ConnectedDivision = div;

                var captain = div.GetCaptain();
                var captainNode = node.transform.Find("CommanderNode");
                captainNode.Find("Name").GetComponent<Text>().text = captain.Name;
                captainNode.GetComponent<TownMenuArmyDivUnit>().ConnectedUnit = captain;
                nrUnits++;

                int y = 0;

                foreach (var unit in div.GetUnits())
                {
                    var divUnit = Instantiate(DivUnitTemplate, node.transform);
                    pos.x = 0.0f;
                    pos.y = DivUnitStartY - (y * DivUnitOffset);
                    divUnit.GetComponent<RectTransform>().anchoredPosition = pos;
                    divUnit.GetComponent<TownMenuArmyDivUnit>().ConnectedUnit = unit;

                    divUnit.transform.Find("Name").GetComponent<Text>().text = unit.Name;
                    nrUnits++;
                    y++;
                }

                x++;
            }

            Background.parent.Find("NrDivisions").GetComponent<Text>().text = nrDivisions.ToString();
            Background.parent.Find("NrUnits").GetComponent<Text>().text = nrUnits.ToString();

            var offset = new Vector2();
            if (nrDivisions > MaxDivisionsVisible)
            {
                offset.x = (Offset + NodeWidth) * (nrDivisions - MaxDivisionsVisible);
                offset.x += Content.GetComponent<RectTransform>().offsetMin.x;
                Content.GetComponent<RectTransform>().offsetMax = offset;
            }
        }
    }

    private void Start()
    {
        Load();
        _Instance = this;
    }

    public static void Reload()
    {
        if (_Instance != null) _Instance.Load();
    }
}
