using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Dictionary<int, Transform> enemyInstanceMap;
    Transform closestEnemy;
    private void Start()
    {
        enemyInstanceMap = new Dictionary<int, Transform>();
        GenerateEnemyList();
    }

    private void GenerateEnemyList()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            AddEnemy(enemy.GetInstanceID(), enemy.transform);
    }

    //Get the enemy thats closest to the given position
    public Transform GetClosestEnemy(Vector3 pos)
    {
        if (!enemyInstanceMap.Any()) return null;

        if (closestEnemy != null) closestEnemy.gameObject.GetComponent<EnemyHitMarker>().ToggleHitmarker(false);

        closestEnemy = enemyInstanceMap.First().Value;
        foreach(KeyValuePair<int, Transform> enemy in enemyInstanceMap.Skip(1))
        {
            if (Vector3.Distance(pos, enemy.Value.position) < Vector3.Distance(pos, closestEnemy.position))
                closestEnemy = enemy.Value;
        }

        closestEnemy.gameObject.GetComponent<EnemyHitMarker>().ToggleHitmarker(true);
        return closestEnemy;
    }

    //Add an enemies to the enemy manager list
    public void AddEnemy(int enemyID, Transform enemyObj)
    {
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
