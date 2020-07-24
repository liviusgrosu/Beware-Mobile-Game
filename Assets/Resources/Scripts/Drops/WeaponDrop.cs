using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float shimmerStartTime;
    [SerializeField]
    private EnumDefinitions.WeaponType weaponType;

    private void Awake()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                col.gameObject.GetComponent<WeaponController>().SwitchWeapons(weaponType);
                Destroy(this.gameObject);
                break;
        }
    }
}
