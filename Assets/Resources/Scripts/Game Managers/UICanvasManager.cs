using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : MonoBehaviour
{
    private Camera uiCam;
    // Start is called before the first frame update
    void Start()
    {
        uiCam = GameObject.Find("UI Camera").GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = uiCam;
    }
}
