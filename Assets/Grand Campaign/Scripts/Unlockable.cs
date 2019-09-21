using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour {

    public bool IsFogOfWar;
    public int QuestIdx;
    private bool _IsUnlocked = false, _IsInitialized = false;

    private void Update()
    {
        if (!_IsInitialized)
        {
            if (!IsFogOfWar) gameObject.SetActive(false);

            ObjectiveManager.Instance.Register(this, QuestIdx);
            _IsUnlocked = false;
            _IsInitialized = true;
        }
    }

    public void Unlock()
    {
        _IsUnlocked = true;
        if (!IsFogOfWar)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsUnlocked()
    {
        return _IsUnlocked;
    }
}
