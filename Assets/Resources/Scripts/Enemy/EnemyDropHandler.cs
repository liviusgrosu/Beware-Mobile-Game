using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropHandler : MonoBehaviour
{

    [SerializeField]
    private int coinAmount;
    private GameObject coinPrefab;
    private bool isQuitting;

    [SerializeField] private EnumDefinitions.WeaponType[] weaponDrops;
    [SerializeField] private EnumDefinitions.MiscDropTypes[] miscDrops;

    public void DropLoot()
    {
        foreach(EnumDefinitions.WeaponType weapon in weaponDrops)
        {
            GameObject obj = Resources.Load(string.Format("Prefabs/Drops/Weapons/{0} Drop", weapon.ToString())) as GameObject;
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        foreach (EnumDefinitions.MiscDropTypes misc in miscDrops)
        {
            GameObject obj = Resources.Load(string.Format("Prefabs/Drops/Misc/{0} Drop", misc.ToString())) as GameObject;
            Instantiate(obj, transform.position, Quaternion.identity);
        }
            
        coinPrefab = Resources.Load("Prefabs/Drops/Coin Drop") as GameObject;

        for (int i = 0; i < coinAmount; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
