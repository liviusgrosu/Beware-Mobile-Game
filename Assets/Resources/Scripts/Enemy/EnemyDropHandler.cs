using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropHandler : MonoBehaviour
{

    [SerializeField]
    private int coinAmount;
    private GameObject coinPrefab;
    private bool isQuitting;

    [SerializeField]
    private EnumDefinitions.WeaponType[] weapons;


    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            foreach(EnumDefinitions.WeaponType weapon in weapons)
            {
                GameObject obj = Resources.Load(string.Format("Prefabs/Drops/{0} Drop", weapon.ToString())) as GameObject;
                Instantiate(obj, transform.position, Quaternion.identity);
            }

            coinPrefab = Resources.Load("Prefabs/Drops/Coin Drop") as GameObject;

            for (int i = 0; i < coinAmount; i++)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
