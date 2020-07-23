using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject currentWeapon;

    [SerializeField]
    private Transform weaponSpawn;

    //TEST
    [SerializeField] 
    GameObject[] testWeapons;

    private void Start()
    {
        SwitchWeapons(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapons(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapons(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapons(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapons(3);
    }

    private void SwitchWeapons(int nextWeapon)
    {
        if (nextWeapon < testWeapons.Length)
        {
            Destroy(currentWeapon);
            currentWeapon = Instantiate(testWeapons[nextWeapon], weaponSpawn.position, weaponSpawn.rotation);
            currentWeapon.transform.parent = weaponSpawn;
        }
    }

    public void ToggleWeaponFire(bool state)
    {
        currentWeapon.GetComponent<Weapon>().ToggleFiring(state);
    }

    public bool IsFiringWeapon()
    {
        return currentWeapon.GetComponent<Weapon>().isFiring;
    }
}
