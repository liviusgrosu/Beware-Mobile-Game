using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> enemyList;

    private void Start()
    {
        enemyList = new List<Transform>();
        GenerateEnemyList();
    }

    private void GenerateEnemyList()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemyList.Add(enemy.transform);
    }

    //Get the enemy thats closest to the given position
    public Transform GetClosestEnemy(Vector3 pos)
    {
        if (!enemyList.Any()) return null;

        Transform closestEnemy = enemyList.First();
        foreach(Transform enemy in enemyList.Skip(1))
        {
            if (Vector3.Distance(pos, enemy.position) < Vector3.Distance(pos, closestEnemy.position))
                closestEnemy = enemy;
        }
        return closestEnemy;
    }

    //Add an enemy to the enemy manager list and spawn it 
    void AddEnemy()
    {
    }

    //Remove an enemy from the enemy manager list
    public void RemoveEnemy(string enemyName)
    {
        Transform destoryEnemy = enemyList.SingleOrDefault(r => r.name.Equals(enemyName));
        if (destoryEnemy == null) return;
        enemyList.Remove(destoryEnemy);
        Destroy(destoryEnemy.gameObject);
    }
}
