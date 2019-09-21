using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public Text MoneyUI;

    private static MoneyManager _Instance;
    public static MoneyManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    private float _Money = 0.0f;

    private void Awake()
    {
        _Instance = this;
    }

    private void Update()
    {
        MoneyUI.text = ((int)_Money).ToString();
    }

    public float GetCurrentMoney()
    {
        return _Money;
    }

    public void AddMoney(float amount)
    {
        _Money += amount;
    }

    public void RemoveMoney(float amount)
    {
        _Money -= amount;
    }
}
