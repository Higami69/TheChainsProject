using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetCamera : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
