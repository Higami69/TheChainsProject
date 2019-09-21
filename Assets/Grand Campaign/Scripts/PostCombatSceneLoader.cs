using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostCombatSceneLoader : MonoBehaviour {

    private void Start()
    {
        if(BattleResults.Instance.Result)
        {
            GameObject.Find("Result").GetComponent<Text>().text = "VICTORY";
        }
        else
        {
            GameObject.Find("Result").GetComponent<Text>().text = "DEFEAT";
        }

        GameObject.Find("PlayerArmy").GetComponent<Text>().text = BattleResults.Instance.PlayerArmySize.ToString();
        GameObject.Find("EnemyArmy").GetComponent<Text>().text = BattleResults.Instance.EnemyArmySize.ToString();
    }
}
