using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReopenQuest : MonoBehaviour {

    public void Click()
    {
        ObjectiveManager.Instance.DisplayQuestFull();
    }
}
