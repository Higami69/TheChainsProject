﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    public void Click()
    {
        SceneManager.LoadScene("GC_MainScene",LoadSceneMode.Single);
    }
}
