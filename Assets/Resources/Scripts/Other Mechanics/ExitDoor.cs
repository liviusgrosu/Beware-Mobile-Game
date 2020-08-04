using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private Material OpenMaterial;
    [SerializeField] private Material ClosedMaterial;
    private Renderer rend;

    private EnemyManager enemyManager;

    [HideInInspector]
    public bool playerFinished;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = ClosedMaterial;
    }

    private void Start()
    {
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (!enemyManager.IsMoreEnemies())
            rend.material = OpenMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!enemyManager.IsMoreEnemies())
            playerFinished = true;
    }
}
