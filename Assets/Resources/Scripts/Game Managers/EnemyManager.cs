using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyWaveSystem waveSystem;

    public Dictionary<int, Transform> enemyInstanceMap;
    Transform currentClosestEnemy, previousClosestEnemy;

    private int? expectedWaveCount = 0;
    private bool levelIsFinished;
    private void Start()
    {
        waveSystem = GameObject.Find("Enemy Wave System").GetComponent<EnemyWaveSystem>();
        enemyInstanceMap = new Dictionary<int, Transform>();
    }

    private void Update()
    {
        if (!levelIsFinished && !IsMoreEnemies() && expectedWaveCount <= 0)
        {
            expectedWaveCount = waveSystem.RequestWave();
            if (expectedWaveCount == null)
            {
                levelIsFinished = true;
                return;
            }
        }
    }

    //Get the enemy thats closest to the given position
    public Transform GetClosestEnemy(Vector3 pos)
    {
        if (!enemyInstanceMap.Any()) return null;
        if (currentClosestEnemy != null) currentClosestEnemy.gameObject.GetComponent<EnemyHitMarker>().ToggleHitmarker(false);

        currentClosestEnemy = enemyInstanceMap.First().Value;
        foreach(KeyValuePair<int, Transform> enemy in enemyInstanceMap.Skip(1))
        {
            if (Vector3.Distance(pos, enemy.Value.position) <Vector3.Distance(pos, currentClosestEnemy.position))
                currentClosestEnemy = enemy.Value;
        }

        currentClosestEnemy.gameObject.GetComponent<EnemyHitMarker>().ToggleHitmarker(true);
        return currentClosestEnemy;
    }

    //Add an enemies to the enemy manager list
    public void AddEnemy(int enemyID, Transform enemyObj)
    {
        if (enemyInstanceMap.ContainsKey(enemyID)) return;
        expectedWaveCount--;
        enemyInstanceMap.Add(enemyID, enemyObj);
    }

    //Remove an enemy from the enemy manager list
    public void RemoveEnemy(int enemyID)
    {
        Transform removedObj = enemyInstanceMap.SingleOrDefault(r => r.Key.Equals(enemyID)).Value;
        if (removedObj == null) return;
        enemyInstanceMap.Remove(enemyID);
        Destroy(removedObj.gameObject);
    }

    public bool IsMoreEnemies()
    {
        // This would change when the wave system is implemented
        return enemyInstanceMap.Count != 0;
    }
}
