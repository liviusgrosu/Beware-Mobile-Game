using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [SerializeField]
    private Text curAmmoCountText, maxAmmoCountText;
    [SerializeField]
    private Image weaponIconImg;

    private WeaponController weaponController;

    private void Start()
    {
        weaponController = GameObject.Find("Player").GetComponent<WeaponController>();
    }

    private void Update()
    {
        curAmmoCountText.text = weaponController.CurrentProjectileCount().ToString();
    }

    public void ChangeWeaponInfo(Sprite weaponIcon, int maxWeaponCount)
    {
        weaponIconImg.sprite = weaponIcon;
        maxAmmoCountText.text = maxWeaponCount.ToString();
    }
}
