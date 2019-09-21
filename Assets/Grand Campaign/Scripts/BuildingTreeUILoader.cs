using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTreeUILoader : MonoBehaviour {

    public GameObject NodeTemplate;
    public Transform Content; 
    public float Offset = 80.0f;
    private float NodeWidth = 160.0f;

    private const int maxFitX = 4, maxFitY = 3;

    private void Start()
    {
        int width = 0;
        int height = 0;

        foreach (var node in BuildingTreeToLoad.Instance)
        {
            if (node.IsUnlocked)
            {
                GameObject newObj = Instantiate(NodeTemplate, Content);
                var nodeScript = newObj.GetComponent<BuildingTreeUINode>();
                nodeScript._Node = node;

                float x = (Offset / 2) + ((node.Id.x - 1) * (Offset + NodeWidth));
                float y = -((Offset / 2) + ((node.Id.y - 1) * (Offset + NodeWidth)));
                newObj.transform.Translate(x, y, 0);

                if (node.Id.x > width) width = node.Id.x;
                if (node.Id.y > height) height = node.Id.y;

                newObj.transform.Find("Cost_Amount").GetComponent<Text>().text = node.Cost.ToString();
                newObj.transform.Find("BuildTime").GetComponent<Text>().text = node.BuildTime.ToString();
            }
        }

        Vector2 size = new Vector2();
        if (width > maxFitX) size.x -= (Offset + NodeWidth) * (width - maxFitX);
        if (height > maxFitY) size.y -= (Offset + NodeWidth) * (height - maxFitY);
        Content.GetComponent<RectTransform>().offsetMax = size;
    }
}
