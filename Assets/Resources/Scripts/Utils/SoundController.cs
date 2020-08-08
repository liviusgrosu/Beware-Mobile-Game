using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSrc;

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip enemySpawnSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip enemyWallBounceSound;

    [Header("Weapon Sound")]
    [SerializeField] private AudioClip pistolSound;
    [SerializeField] private AudioClip sniperSound;
    [SerializeField] private AudioClip shotgunSound;
    [SerializeField] private AudioClip chainSound;

    [Header("Player Sound")]
    [SerializeField] private AudioClip playerHitSound;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayEnemySpawnSound()
    {
        audioSrc.PlayOneShot(enemySpawnSound);
    }

    public void PlayEnemyDeathSound()
    {
        audioSrc.PlayOneShot(enemyDeathSound);
    }

    public void PlayEnemyWallBounce()
    {
        audioSrc.PlayOneShot(enemyWallBounceSound);
    }

    public void PlayWeaponFireSound(EnumDefinitions.WeaponType weapon)
    {
        switch (weapon)
        {
            case EnumDefinitions.WeaponType.Pistol:
                audioSrc.PlayOneShot(pistolSound);
                break;
            case EnumDefinitions.WeaponType.Sniper:
                audioSrc.PlayOneShot(sniperSound);
                break;
            case EnumDefinitions.WeaponType.Shotgun:
                audioSrc.PlayOneShot(shotgunSound);
                break;
            case EnumDefinitions.WeaponType.Chaingun:
                audioSrc.PlayOneShot(chainSound);
                break;
        }
    }
    
    public void PlayPlayerHitSound()
    {
        audioSrc.PlayOneShot(playerHitSound);
    }
}
