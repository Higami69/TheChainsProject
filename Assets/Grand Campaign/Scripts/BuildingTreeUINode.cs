using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTreeUINode : MonoBehaviour {

    public BuildingTreeNode _Node;
    private Image _Image;
    private Text _BuildTimeText;
    private Text _Name;
    private Text _Description;

	void Start () {
        _Image = GetComponent<Image>();
        _BuildTimeText = transform.Find("BuildTime").GetComponent<Text>();
        _Name = transform.Find("Name").GetComponent<Text>();
        _Description = transform.Find("Description").GetComponent<Text>();
	}
	
	void Update () {
        _Image.color = Color.grey;

        if (_Node.IsActivated()) _Image.color = Color.green;
        if (_Node.IsBuilding())
        {
            _Image.color = Color.yellow;
            _BuildTimeText.text = _Node.GetBuildTimeRemaining().ToString();
        }
        if (_Node.IsPurchased())
        {
            _Image.color = Color.red;
            _BuildTimeText.text = "";
        }

        _Name.text = _Node.AttachedBuilding.GetName();
        _Description.text = _Node.AttachedBuilding.GetDescription();
    }

    public void HandleClick()
    {
        if (!_Node.IsActivated() || _Node.IsBuilding() || _Node.IsPurchased()) return;

        _Node.Purchase();
        TownMenuBuildingLoader.Reload();
        RegionMenuBuildingSceneLoader.Reload();
    }
}
