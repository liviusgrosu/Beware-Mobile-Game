using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    private IEnemyMovement enemyMovement;
    private EnemyHealthSystem healthSystem;
    private EnemyDropHandler dropHandler;

    // Start is called before the first frame update
    void Awake()
    {
        enemyMovement = GetComponent<IEnemyMovement>();
        healthSystem = GetComponent<EnemyHealthSystem>();
        dropHandler = GetComponent<EnemyDropHandler>();
    }

    public void InitObject()
    {

    }
}
