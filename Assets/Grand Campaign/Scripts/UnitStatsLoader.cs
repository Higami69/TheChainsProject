using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatsLoader : MonoBehaviour {
    public static GC_Unit UnitToLoad { get; set; }
    public Transform Background;
    private static UnitStatsLoader _Instance;

    private void Start()
    {
        _Instance = this;
        Load();
    }

    public void Load()
    {
        Background.Find("Name").GetComponent<Text>().text = UnitToLoad.Name;
        Background.Find("Health").GetComponent<Text>().text = UnitToLoad.GetStats().Health.ToString();
        Background.Find("Morale").GetComponent<Text>().text = UnitToLoad.GetStats().Morale.ToString();
        Background.Find("Speed").GetComponent<Text>().text = UnitToLoad.GetStats().Speed.ToString();
        Background.Find("Range").GetComponent<Text>().text = UnitToLoad.GetStats().Range.ToString();
        Background.Find("MeleeAtk").GetComponent<Text>().text = UnitToLoad.GetStats().Melee_Attack.ToString();
        Background.Find("MeleeDef").GetComponent<Text>().text = UnitToLoad.GetStats().Melee_Defense.ToString();
        Background.Find("RangedAtk").GetComponent<Text>().text = UnitToLoad.GetStats().Ranged_Attack.ToString();
        Background.Find("RangedDef").GetComponent<Text>().text = UnitToLoad.GetStats().Ranged_Defense.ToString();
    }

    public static void Reload()
    {
       if(_Instance != null) _Instance.Load();
    }

}
