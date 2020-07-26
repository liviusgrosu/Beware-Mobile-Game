using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariantHandler : MonoBehaviour
{
    [SerializeField] private bool containsVariant;
    [SerializeField] private GameObject varientPrefab;
    [SerializeField] private int variantAmount;

    EnemyManager enemyManager;

    private bool isQuitting;

    private void Start()
    {
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting && containsVariant)
        {
            for(int i = 0; i < variantAmount; i++)
            {
                GameObject variantObj = Instantiate(varientPrefab, transform.position, Quaternion.identity);
                enemyManager.AddEnemy(variantObj.GetInstanceID(), variantObj.transform);
            }
        }
    }
}
