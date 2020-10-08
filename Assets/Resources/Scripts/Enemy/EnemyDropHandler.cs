using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropHandler : MonoBehaviour
{
    public int coinAmount;
    private GameObject coinPrefab;
    private bool isQuitting;

    public EnumDefinitions.WeaponType weaponDrop;
    public EnumDefinitions.MiscDropTypes miscDrops;

    public void DropLoot()
    {
        if (weaponDrop != EnumDefinitions.WeaponType.None)
        {
            GameObject obj = Resources.Load(string.Format("Prefabs/Drops/Weapons/{0} Drop", weaponDrop.ToString())) as GameObject;
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        if (miscDrops != EnumDefinitions.MiscDropTypes.None)
        {
            GameObject obj = Resources.Load(string.Format("Prefabs/Drops/Misc/{0} Drop", miscDrops.ToString())) as GameObject;
            Instantiate(obj, transform.position, Quaternion.identity);
        }
            
        coinPrefab = Resources.Load("Prefabs/Drops/Misc/Coin Drop") as GameObject;

        for (int i = 0; i < coinAmount; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
