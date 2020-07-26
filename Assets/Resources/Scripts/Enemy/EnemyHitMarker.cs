using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitMarker : MonoBehaviour
{
    public GameObject hitmarkerUI;

    public void ToggleHitmarker(bool state)
    {
        hitmarkerUI.SetActive(state);
    }
}
