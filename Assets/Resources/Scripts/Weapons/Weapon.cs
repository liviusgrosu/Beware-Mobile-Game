using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Schema;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SoundController audioController;
    [SerializeField] private EnumDefinitions.WeaponType weaponType;

    [Header("Projectile Properties")]
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    [Header("Projectile Count")]
    public bool isAmmoInfinite;
    public int maxProjectileCount;
    private int _curProjectileCount;

    public int curProjectileCount {
        get { return _curProjectileCount; }
        set {
            _curProjectileCount = value;
            if (!isAmmoInfinite && _curProjectileCount <= 0) Destroy(this.gameObject);
        }
    }

    [Header("Spawns")]
    [SerializeField]
    private Transform[] bulletSpawns;
    private int bulletSpawnIndex = 0;

    [HideInInspector]
    public bool isFiring;

    private const float BULLET_LIFE = 1.2f;

    [Header("Misc")]
    public Sprite weaponIcon;

    private Animator entityAnimator;

    private enum FireType
    {
        Sequential,
        Random,
        All
    }

    [SerializeField]
    private FireType fireType;

    private void Awake()
    {
        curProjectileCount = maxProjectileCount;
        isFiring = false;
    }

    private void Update()
    {
        if(audioController == null)
        {
            audioController = GameObject.Find("Sound Controller").GetComponent<SoundController>();
        }
    }

    public void ToggleFiring(bool state)
    {
        isFiring = state;
        if (isFiring)
        {
            StartCoroutine(BeginFiring());
        }
        else
        {
            StopAllCoroutines();
        }
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
        if (entityAnimator != null)
        {
            entityAnimator.SetTrigger("Fire");
        }
        if (audioController != null) 
        {
            audioController.PlayWeaponFireSound(weaponType);
        }
        if (!isAmmoInfinite) curProjectileCount--;
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
                {
                    InstantiateBullet();
                }
                break;
        }
    }

    private void InstantiateBullet()
    {
        GameObject bulletInstant = Instantiate(projectilePrefab, bulletSpawns[bulletSpawnIndex].position, Quaternion.identity);
        bulletInstant.GetComponent<Rigidbody>().AddForce(bulletSpawns[bulletSpawnIndex].forward * projectileSpeed);
        Destroy(bulletInstant, BULLET_LIFE);
    }

    public void PassEntityAnimator(Animator entityAnimator)
    {
        this.entityAnimator = entityAnimator;
    }
}
