using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldArmy : MonoBehaviour {
    public GC_Army Army;
    public bool HasMoved;

    private const float yPos = 0.3f;
    private const float battleRadius = 1.0f;
    private bool _IsInitialized = false;

    private void Update()
    {
        if (_IsInitialized) return;

        Army.Position = transform.position;
        if (GetComponent<Ownership>().Owner == Owner.PLAYER)
        {
            GetComponent<Renderer>().material.color = Color.green;
            TurnManager.Instance.RegisterArmy(this);
        }
        else if (GetComponent<Ownership>().Owner == Owner.ENEMY_1)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        _IsInitialized = true;
    }
    public void MoveTo(Vector3 newPos)
    {
        Vector3 pos = new Vector3(newPos.x, yPos, newPos.z);
        transform.position = pos;
        HasMoved = true;
        Army.Position = pos;
        ArmyRangeDisplay.Instance.SetSize(0);
        CheckForBattle();
    }

    public void HandleTurnEnd()
    {
        HasMoved = false;
    }

    public void CheckForBattle()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Town"))
        {
            if (obj.GetComponent<Ownership>().Owner != Owner.PLAYER && 
                Vector3.Distance(transform.position, obj.transform.position) <= battleRadius)
            {
                var town = obj.GetComponent<Town>();
                if (SimulateBattle(town.GetGarrison()))
                {
                    town.ClearGarrison();
                    town.GetComponent<Ownership>().Owner = Owner.PLAYER;
                }
                else
                {
                    Army.Unregister();
                    Destroy(gameObject);
                    Player.Instance.CloseArmyMenus();
                }

                Player.Instance.SetTreeOpen(true);
                SceneManager.LoadSceneAsync("GC_PostCombatScene", LoadSceneMode.Additive);
            }
        }
        foreach (var obj in GameObject.FindGameObjectsWithTag("Army"))
        {
            if (obj.GetComponent<Ownership>().Owner != Owner.PLAYER &&
                Vector3.Distance(transform.position, obj.transform.position) <= battleRadius)
            {
                if(SimulateBattle(obj.GetComponent<OverworldArmy>().Army))
                {
                    Destroy(obj);
                }
                else
                {
                    Army.Unregister();
                    Destroy(gameObject);
                    Player.Instance.CloseArmyMenus();
                }

                Player.Instance.SetTreeOpen(true);
                SceneManager.LoadSceneAsync("GC_PostCombatScene", LoadSceneMode.Additive);
            }
        }
    }

    private bool SimulateBattle(GC_Army enemy)
    {
        //TODO: Maybe actually do some sort of simulating
        BattleResults.Instance.PlayerArmySize = Army.GetSize();
        BattleResults.Instance.EnemyArmySize = enemy.GetSize();


        if (Army.GetSize() > enemy.GetSize())
        {
            BattleResults.Instance.Result = true;
            return true;
        }
        else
        {
            BattleResults.Instance.Result = false;
            return false;
        }
    }

    public Region GetRegion()
    {
        foreach (var region in GameObject.FindGameObjectsWithTag("Region"))
        {
            if (region.GetComponent<Region>().IsPointInRegion(Army.Position))
                return region.GetComponent<Region>();
        }
        return null;
    }
}

