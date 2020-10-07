using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour, IUIGenericElement
{
    private bool isUIActive;

    [SerializeField]
    private Text curAmmoCountText;
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
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }
}
