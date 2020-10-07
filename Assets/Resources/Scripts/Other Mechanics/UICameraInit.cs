using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }
}
