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

    [Header("Weapon Sounds")]
    [SerializeField] private AudioClip pistolSound;
    [SerializeField] private AudioClip sniperSound;
    [SerializeField] private AudioClip shotgunSound;
    [SerializeField] private AudioClip chainSound;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip playerHitSound;

    [Header("Drop Sounds")]
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip weaponPickupSound;
    [SerializeField] private AudioClip healthPickupSound;

    [Header("Movement Sounds")]
    [SerializeField] private AudioClip regularMovementSound;
    [SerializeField] private AudioClip slimeMovementSound;
    [SerializeField] private AudioClip wingFlapMovementSound;

    [Header("Menu Sounds")]
    [SerializeField] private AudioClip starCollectSound;
    [SerializeField] private AudioClip buttonPressSound;

    [SerializeField] private AudioClip doorOpenSound;
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

    public void PlayMovementSound(EnumDefinitions.MovementSoundTypes type)
    {
        switch (type)
        {
            case EnumDefinitions.MovementSoundTypes.Grass:
                audioSrc.PlayOneShot(regularMovementSound);
                break;
            case EnumDefinitions.MovementSoundTypes.Slime:
                audioSrc.PlayOneShot(slimeMovementSound);
                break;
            case EnumDefinitions.MovementSoundTypes.WingFlap:
                audioSrc.PlayOneShot(wingFlapMovementSound);
                break;
        }
    }

    public void PlayMenuSound(EnumDefinitions.MenuSoundClip type)
    {
        switch(type)
        {
            case EnumDefinitions.MenuSoundClip.ButtonPress:
                audioSrc.PlayOneShot(buttonPressSound);
                break;
            case EnumDefinitions.MenuSoundClip.StarCollect:
                audioSrc.PlayOneShot(starCollectSound);
                break;
        }
    }

    public void PlayCoinPickUpSound()
    {
        audioSrc.PlayOneShot(coinPickupSound);
    }

    public void PlayWeaponPickUpSound()
    {
        audioSrc.PlayOneShot(weaponPickupSound);
    }

    public void PlayHPPickUpSound()
    {
        audioSrc.PlayOneShot(healthPickupSound);
    }

    public void PlayerDoorOpenSound()
    {
        audioSrc.PlayOneShot(doorOpenSound);
    }
}

