using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    private static Player _Instance;
    public static Player Instance
    {
        get
        {
            return _Instance;
        }
    }

    private Camera _Camera;

    public float StartingMoney = 500.0f;
    public float MoveSpeed = 10.0f;
    public GameObject ArmyTemplate;

    private const float _MaxRayDistance = 99999.0f;
    private const int _RayLayerMask = 1 << 9;

    private bool _IsTreeOpen = false;
    private bool _WasTreeOpen = false;
    private bool _IsTownMenuOpen = false;
    private bool _IsRegionMenuOpen = false;
    private bool _IsArmyMenuOpen = false;
    public Town SelectedTown;
    public Region SelectedRegion;
    public GC_Army SelectedArmy;
    public bool IsUnitStatsScreenOpen = false;

    private Dictionary<string, int> _OwnedUnits = new Dictionary<string, int>();

    void Start () {
        _Instance = this;
        _Camera = Camera.main;
        MoneyManager.Instance.AddMoney(StartingMoney);
	}
	

	void Update () {
        //Handle Movement
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocity.Normalize();
        velocity *= MoveSpeed * Time.deltaTime;

        transform.Translate(velocity,Space.World);

        //Handle Selection
        if (Input.GetButtonUp("Pause") && !_WasTreeOpen)
        {
            SetTreeOpen(true);
            SceneManager.LoadSceneAsync("GC_PauseMenu", LoadSceneMode.Additive);
        }
        if (Input.GetButtonDown("LMClick") && !_WasTreeOpen)
        {
            if (Input.mousePosition.y < 250.0f) return;

            var ray = _Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, _MaxRayDistance, _RayLayerMask))
            {
                if (_IsRegionMenuOpen)
                {
                    RegionMenuMasterSelector.Unload();
                    SceneManager.UnloadSceneAsync("GC_RegionMenuMaster");
                    _IsRegionMenuOpen = false;
                }

                if (rayHit.transform.GetComponent<Ownership>().Owner == Owner.PLAYER)
                {
                    if (rayHit.transform.CompareTag("Town"))
                    {
                        if (_IsArmyMenuOpen)
                        {
                            SceneManager.UnloadSceneAsync("GC_ArmyMenuMaster");
                            SceneManager.UnloadSceneAsync("GC_ArmyMenuArmy");
                            SelectedArmy = null;

                            _IsArmyMenuOpen = false;

                            if (IsUnitStatsScreenOpen)
                            {
                                SceneManager.UnloadSceneAsync("GC_UnitStats");
                                IsUnitStatsScreenOpen = false;
                            }
                        }

                        if (SelectedTown == null)
                        {
                            SelectedTown = rayHit.transform.GetComponent<Town>();
                            _IsTownMenuOpen = true;
                            SceneManager.LoadScene("GC_TownMenuMaster", LoadSceneMode.Additive);
                        }
                        var town = rayHit.transform.GetComponent<Town>();
                        if (SelectedTown != town)
                        {
                            SelectedTown = town;
                            TownMenuMasterSelector.Reload();
                        }

                        SelectedTown.gameObject.GetComponent<SelectableArea>().Select();
                    }
                    else if (rayHit.transform.CompareTag("Army"))
                    {
                        if (_IsTownMenuOpen)
                        {
                            SelectionDrawer.ResetPoints(0);
                            SelectedTown = null;
                            _IsTownMenuOpen = false;
                            TownMenuMasterSelector.Unload();
                            SceneManager.UnloadSceneAsync("GC_TownMenuMaster");
                            if (IsUnitStatsScreenOpen)
                            {
                                SceneManager.UnloadSceneAsync("GC_UnitStats");
                                IsUnitStatsScreenOpen = false;
                            }
                        }
                        if (_IsRegionMenuOpen)
                        {
                            SelectionDrawer.ResetPoints(0);
                            _IsRegionMenuOpen = false;

                            RegionMenuMasterSelector.Unload();
                            SceneManager.UnloadSceneAsync("GC_RegionMenuMaster");
                            if (IsUnitStatsScreenOpen)
                            {
                                SceneManager.UnloadSceneAsync("GC_UnitStats");
                                IsUnitStatsScreenOpen = false;
                            }
                        }

                        var army = rayHit.transform.GetComponent<OverworldArmy>().Army;
                        if (SelectedArmy == null)
                        {
                            SelectedArmy = army;
                            SceneManager.LoadSceneAsync("GC_ArmyMenuMaster", LoadSceneMode.Additive);
                            SceneManager.LoadSceneAsync("GC_ArmyMenuArmy", LoadSceneMode.Additive);
                            _IsArmyMenuOpen = true;
                        }
                        else if (SelectedArmy != army)
                        {
                            SelectedArmy = army;
                            SceneManager.UnloadSceneAsync("GC_ArmyMenuMaster");
                            SceneManager.UnloadSceneAsync("GC_ArmyMenuArmy");

                            SceneManager.LoadSceneAsync("GC_ArmyMenuMaster", LoadSceneMode.Additive);
                            SceneManager.LoadSceneAsync("GC_ArmyMenuArmy", LoadSceneMode.Additive);
                        }

                        if (SelectedArmy != null && !rayHit.transform.GetComponent<OverworldArmy>().HasMoved)
                        {
                            army.DisplayRange();
                        }
                    }
                }
                else if (_IsTownMenuOpen)
                {
                    SelectionDrawer.ResetPoints(0);
                    SelectedTown = null;
                    _IsTownMenuOpen = false;
                    TownMenuMasterSelector.Unload();
                    SceneManager.UnloadSceneAsync("GC_TownMenuMaster");
                    if (IsUnitStatsScreenOpen)
                    {
                        SceneManager.UnloadSceneAsync("GC_UnitStats");
                        IsUnitStatsScreenOpen = false;
                    }
                }
                else if (_IsRegionMenuOpen)
                {
                    SelectionDrawer.ResetPoints(0);
                    _IsRegionMenuOpen = false;

                    RegionMenuMasterSelector.Unload();
                    SceneManager.UnloadSceneAsync("GC_RegionMenuMaster");
                    if (IsUnitStatsScreenOpen)
                    {
                        SceneManager.UnloadSceneAsync("GC_UnitStats");
                        IsUnitStatsScreenOpen = false;
                    }
                }
                else if (_IsArmyMenuOpen)
                {
                    SceneManager.UnloadSceneAsync("GC_ArmyMenuMaster");
                    SceneManager.UnloadSceneAsync("GC_ArmyMenuArmy");
                    SelectedArmy = null;

                    _IsArmyMenuOpen = false;

                    if (IsUnitStatsScreenOpen)
                    {
                        SceneManager.UnloadSceneAsync("GC_UnitStats");
                        IsUnitStatsScreenOpen = false;
                    }
                }
            }
            else if (_IsTownMenuOpen)
            {
                SelectionDrawer.ResetPoints(0);
                SelectedTown = null;
                _IsTownMenuOpen = false;
                TownMenuMasterSelector.Unload();
                SceneManager.UnloadSceneAsync("GC_TownMenuMaster");
                if (IsUnitStatsScreenOpen)
                {
                    SceneManager.UnloadSceneAsync("GC_UnitStats");
                    IsUnitStatsScreenOpen = false;
                }
            }
            else if (_IsRegionMenuOpen)
            {
                SelectionDrawer.ResetPoints(0);
                _IsRegionMenuOpen = false;

                RegionMenuMasterSelector.Unload();
                SceneManager.UnloadSceneAsync("GC_RegionMenuMaster");
                if (IsUnitStatsScreenOpen)
                {
                    SceneManager.UnloadSceneAsync("GC_UnitStats");
                    IsUnitStatsScreenOpen = false;
                }
            }
            else if (_IsArmyMenuOpen)
            {
                SceneManager.UnloadSceneAsync("GC_ArmyMenuMaster");
                SceneManager.UnloadSceneAsync("GC_ArmyMenuArmy");
                SelectedArmy = null;

                _IsArmyMenuOpen = false;

                if (IsUnitStatsScreenOpen)
                {
                    SceneManager.UnloadSceneAsync("GC_UnitStats");
                    IsUnitStatsScreenOpen = false;
                }
            }
        }
        if(Input.GetButtonDown("RMClick") && SelectedArmy != null)
        {
            if (Input.mousePosition.y < 250.0f) return;

            int mask = 1 << 10;
            var ray = _Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, _MaxRayDistance, mask))
            {
                if(rayHit.transform.CompareTag("Terrain") && Vector3.Distance(rayHit.point, SelectedArmy.Position) <= SelectedArmy.GetMoveRange())
                {
                    if(SelectedArmy.InOverWorld)
                    {
                        if(!SelectedArmy.GetOverworldArmy().HasMoved)
                            SelectedArmy.GetOverworldArmy().MoveTo(rayHit.point);
                    }
                    else
                    {
                        var obj = Instantiate(ArmyTemplate);
                        obj.GetComponent<OverworldArmy>().Army = SelectedArmy;
                        obj.GetComponent<OverworldArmy>().MoveTo(rayHit.point);
                        SelectedArmy.SetOverworldArmy(obj.GetComponent<OverworldArmy>());
                        SelectedArmy.InOverWorld = true;
                        SelectedTown.ClearGarrison();
                        SelectedArmy = null;
                    }
                }
            }
        }

        if (!_IsTreeOpen && _WasTreeOpen) _WasTreeOpen = false;
	}

    public void SetTreeOpen(bool isTreeOpen)
    {
        _IsTreeOpen = isTreeOpen;
        if (isTreeOpen) _WasTreeOpen = true;
    }

    public void OpenRegion()
    {
        _IsTownMenuOpen = false;
        _IsRegionMenuOpen = true;
    }

    public void AddUnit(string name)
    {
        if(_OwnedUnits.ContainsKey(name))
        {
            _OwnedUnits[name]++;
        }
        else
        {
            _OwnedUnits.Add(name, 1);
        }
    }

    public void RemoveUnit(string name)
    {
        if (_OwnedUnits.ContainsKey(name)) _OwnedUnits[name]--;
    }

    public int GetUnitAmount(string name)
    {
        if (_OwnedUnits.ContainsKey(name))
            return _OwnedUnits[name];
        else return 0;
    }

    public void CloseArmyMenus()
    {
        if (_IsArmyMenuOpen)
        {
            SceneManager.UnloadSceneAsync("GC_ArmyMenuMaster");
            SceneManager.UnloadSceneAsync("GC_ArmyMenuArmy");
            SelectedArmy = null;

            _IsArmyMenuOpen = false;

            if (IsUnitStatsScreenOpen)
            {
                SceneManager.UnloadSceneAsync("GC_UnitStats");
                IsUnitStatsScreenOpen = false;
            }
        }
    }
}
