using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Schema;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    public int maxProjectileCount;
    private int _curProjectileCount;

    public int curProjectileCount {
        get { return _curProjectileCount; }
        set { 
            _curProjectileCount = value;
            if (_curProjectileCount <= 0) Destroy(this.gameObject);
        }
    }

    [SerializeField] 
    private Transform[] bulletSpawns;
    private int bulletSpawnIndex = 0;

    [HideInInspector]
    public bool isFiring;

    private const float BULLET_LIFE = 1.2f;

    private enum FireType
    {
        Sequential, 
        Random, 
        All
    }

    [SerializeField]
    private FireType fireType;

    private void Start()
    {
        curProjectileCount = maxProjectileCount;
        isFiring = false;
    }

    public void ToggleFiring(bool state)
    {
        isFiring = state;
        if (isFiring) 
            StartCoroutine(BeginFiring());
        else 
            StopAllCoroutines();
    }

    IEnumerator BeginFiring()
    {
        while (isFiring)
        {
            FireProjectile();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void FireProjectile()
    {
        switch (fireType)
        {
            case FireType.Sequential:
                InstantiateBullet();
                bulletSpawnIndex = bulletSpawnIndex >= bulletSpawns.Length ? bulletSpawnIndex + 1 : 0;
                break;
            case FireType.Random:
                bulletSpawnIndex = UnityEngine.Random.Range(0, bulletSpawns.Length);
                InstantiateBullet();
                break;
            case FireType.All:
                for (bulletSpawnIndex = 0; bulletSpawnIndex < bulletSpawns.Length; bulletSpawnIndex++)
                    InstantiateBullet();
                break;
        }
    }

    private void InstantiateBullet()
    {
        GameObject bulletInstant = Instantiate(projectilePrefab, bulletSpawns[bulletSpawnIndex].position, Quaternion.identity);
        bulletInstant.GetComponent<Rigidbody>().AddForce(bulletSpawns[bulletSpawnIndex].forward * projectileSpeed);
        Destroy(bulletInstant, BULLET_LIFE);
    }
}
