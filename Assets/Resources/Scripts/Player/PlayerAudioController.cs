using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSrc;

    [Header("Weapon Sound")]
    [SerializeField] private AudioClip pistolSound;
    [SerializeField] private AudioClip sniperSound;
    [SerializeField] private AudioClip shotgunSound;
    [SerializeField] private AudioClip chainSound;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayWeaponFireSound(EnumDefinitions.WeaponType weapon)
    {
        switch(weapon)
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
}
