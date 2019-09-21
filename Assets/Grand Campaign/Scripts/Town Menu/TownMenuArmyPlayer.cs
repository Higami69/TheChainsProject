using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TownMenuArmyPlayer : MonoBehaviour {

    public GameObject UnitTemplate;
    private EventSystem _EventSystem;
    private GameObject _CurrentGrabbedObject;
    private GC_Unit _SelectedUnit;
    private GC_Division _DefaultDiv;
    private GC_Army _CurrentArmy;
    private Transform _Canvas;
    private bool _IsMouseDown = false;

    private void Awake()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        if (Player.Instance.SelectedTown != null)
            _CurrentArmy = Player.Instance.SelectedTown.GetGarrison();
        else if (Player.Instance.SelectedArmy != null)
            _CurrentArmy = Player.Instance.SelectedArmy;

        _EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        _Canvas = GameObject.Find("Canvas").transform;
    }

    void Update () {
		if(Input.GetButtonDown("LMClick"))
        {
            _IsMouseDown = true;
            var pointerEventData = new PointerEventData(_EventSystem);
            pointerEventData.position = Input.mousePosition;

            var results = new List<RaycastResult>();
            _EventSystem.RaycastAll(pointerEventData, results);
            bool unitIsCommander = false;
            bool somethingHappened = false;

            foreach(var result in results)
            {
                var obj = result.gameObject;
                if (obj.CompareTag("TownMenuArmyMovable"))
                {
                    _SelectedUnit = obj.GetComponent<TownMenuArmyDivUnit>().ConnectedUnit;
                    _CurrentGrabbedObject = Instantiate(UnitTemplate, _Canvas);
                    _CurrentGrabbedObject.transform.Find("Name").GetComponent<Text>().text = _SelectedUnit.Name;
                    somethingHappened = true;

                    if(!Player.Instance.IsUnitStatsScreenOpen)
                    {
                        SceneManager.LoadScene("GC_UnitStats", LoadSceneMode.Additive);
                        Player.Instance.IsUnitStatsScreenOpen = true;
                    }
                    UnitStatsLoader.UnitToLoad = _SelectedUnit;
                    UnitStatsLoader.Reload();

                }
                if (obj.CompareTag("TownMenuArmyCommander"))
                {
                    unitIsCommander = true;
                    _SelectedUnit = obj.GetComponent<TownMenuArmyDivUnit>().ConnectedUnit;
                    _CurrentGrabbedObject = Instantiate(UnitTemplate, _Canvas);
                    _CurrentGrabbedObject.transform.Find("Name").GetComponent<Text>().text = _SelectedUnit.Name;
                    somethingHappened = true;

                    if (!Player.Instance.IsUnitStatsScreenOpen)
                    {
                        SceneManager.LoadScene("GC_UnitStats", LoadSceneMode.Additive);
                        Player.Instance.IsUnitStatsScreenOpen = true;
                    }
                    UnitStatsLoader.UnitToLoad = _SelectedUnit;
                    UnitStatsLoader.Reload();
                }
                if(obj.CompareTag("TownMenuArmyGeneral"))
                {
                    if (!Player.Instance.IsUnitStatsScreenOpen)
                    {
                        SceneManager.LoadScene("GC_UnitStats", LoadSceneMode.Additive);
                        Player.Instance.IsUnitStatsScreenOpen = true;
                    }
                    UnitStatsLoader.UnitToLoad = _CurrentArmy.GetGeneral();
                    UnitStatsLoader.Reload();
                }
            }
            foreach(var result in results)
            {
                var obj = result.gameObject;
                if(obj.CompareTag("TownMenuArmyInteract"))
                {
                    var div = obj.GetComponent<TownMenuArmyDivision>().ConnectedDivision;
                    if (!unitIsCommander) div.RemoveUnit(_SelectedUnit);
                    else div.RemoveCaptain();

                    if (div.GetCaptain() != null) _DefaultDiv = div;
                    somethingHappened = true;
                }
            }
            if(somethingHappened) TownMenuArmyLoader.Reload();
        }
        if(Input.GetButtonUp("LMClick") && _IsMouseDown)
        {
            _IsMouseDown = false;

            if (_CurrentGrabbedObject)
            {
                var pointerEventData = new PointerEventData(_EventSystem);
                pointerEventData.position = Input.mousePosition;

                var results = new List<RaycastResult>();
                _EventSystem.RaycastAll(pointerEventData, results);
                bool hitBackground = false;

                foreach (var result in results)
                {
                    var obj = result.gameObject;
                    if (obj.CompareTag("TownMenuArmyInteract"))
                    {
                        obj.GetComponent<TownMenuArmyDivision>().ConnectedDivision.AddUnit(_SelectedUnit);
                        _SelectedUnit = null;
                        _DefaultDiv = null;
                        Destroy(_CurrentGrabbedObject);
                        TownMenuArmyLoader.Reload();

                        return;
                    }
                    if(obj.CompareTag("TownMenuArmyGeneral"))
                    {
                        if(_CurrentArmy.GetGeneral() != null)
                        {
                            var div = new GC_Division();
                            div.AddUnit(_CurrentArmy.GetGeneral());
                            _CurrentArmy.AddDivision(div);
                        }
                        _CurrentArmy.SetGeneral(_SelectedUnit);
                        _SelectedUnit = null;
                        _DefaultDiv = null;
                        Destroy(_CurrentGrabbedObject);
                        TownMenuArmyLoader.Reload();

                        return;
                    }
                    if(obj.CompareTag("TownMenuArmyCommander"))
                    {
                        var div = obj.GetComponent<TownMenuArmyDivUnit>().ConnectedUnit.Division;
                        div.AddUnit(_SelectedUnit);
                        div.SetCaptain(_SelectedUnit);

                        _SelectedUnit = null;
                        _DefaultDiv = null;
                        Destroy(_CurrentGrabbedObject);
                        TownMenuArmyLoader.Reload();

                        return;
                    }
                    if(obj.CompareTag("Background"))
                    {
                        var div = new GC_Division();
                        div.AddUnit(_SelectedUnit);
                        _CurrentArmy.AddDivision(div);
                        hitBackground = true;
                    }
                }

                if (!hitBackground)
                {
                    if(_DefaultDiv != null) _DefaultDiv.AddUnit(_SelectedUnit);
                    else
                    {
                        var div = new GC_Division();
                        div.AddUnit(_SelectedUnit);
                        _CurrentArmy.AddDivision(div);
                    }
                }

                Destroy(_CurrentGrabbedObject);
                _SelectedUnit = null;
                _DefaultDiv = null;
                TownMenuArmyLoader.Reload();
            }
        }

        if(_IsMouseDown && _CurrentGrabbedObject)
        {
            _CurrentGrabbedObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
	}
}
