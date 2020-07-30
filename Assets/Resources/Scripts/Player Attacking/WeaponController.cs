using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject currentWeapon;
    private WeaponUIController uiController;

    [SerializeField]
    private Transform weaponSpawn;

    //TEST
    [SerializeField]
    GameObject[] testWeapons;

    private bool isPlayer;

    private void Start()
    {
        isPlayer = this.gameObject.tag == "Player" ? true : false; 

        uiController = GameObject.Find("Weapon Player UI").GetComponent<WeaponUIController>();
        SwitchWeapons(EnumDefinitions.WeaponType.Pistol);
    }

    private void Update()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapons(EnumDefinitions.WeaponType.Pistol);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapons(EnumDefinitions.WeaponType.Shotgun);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapons(EnumDefinitions.WeaponType.Chaingun);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapons(EnumDefinitions.WeaponType.Sniper);

            if (currentWeapon == null)
                SwitchWeapons(EnumDefinitions.WeaponType.Pistol);
        }
    }

    public void SwitchWeapons(EnumDefinitions.WeaponType nextWeapon)
    {
        if ((int)nextWeapon < testWeapons.Length)
        {
            Destroy(currentWeapon);
            GameObject weaponPrefab = Resources.Load(string.Format("Prefabs/Weapons/{0} Weapon", nextWeapon.ToString())) as GameObject;
            currentWeapon = Instantiate(weaponPrefab, weaponSpawn.position, weaponSpawn.rotation);
            currentWeapon.transform.parent = weaponSpawn;
            uiController.ChangeWeaponInfo(currentWeapon.GetComponent<Weapon>().weaponIcon, currentWeapon.GetComponent<Weapon>().maxProjectileCount);
        }
    }

    public void ToggleWeaponFire(bool state) { if (currentWeapon != null) currentWeapon.GetComponent<Weapon>().ToggleFiring(state); }
    public bool IsFiringWeapon() { return (currentWeapon != null) ? currentWeapon.GetComponent<Weapon>().isFiring : false; }
    public int CurrentProjectileCount() { return currentWeapon.GetComponent<Weapon>().curProjectileCount; }
}
