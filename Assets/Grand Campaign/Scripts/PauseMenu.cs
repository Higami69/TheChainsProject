﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Pause"))
        {
            Player.Instance.SetTreeOpen(false);
            SceneManager.UnloadSceneAsync("GC_PauseMenu");
        }
    }
}
