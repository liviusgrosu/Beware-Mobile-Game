using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariantHandler : MonoBehaviour
{
    [SerializeField] private bool containsVariant;

    private bool isQuitting;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {

        }
    }
}
