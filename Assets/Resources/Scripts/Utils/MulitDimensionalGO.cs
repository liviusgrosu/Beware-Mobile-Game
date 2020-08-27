using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveObject
{
    public EnemyInstaParams[] enemy;
}

[Serializable]
public class EnemyInstaParams
{
    public GameObject enemyObj;

    public bool allowVariant;
    public int variantAmount;
    public GameObject variantObject;

    public EnumDefinitions.WeaponType startingWeapon;

    public EnumDefinitions.WeaponType weaponDrop;
    public int coinDropAmount;
    public EnumDefinitions.MiscDropTypes miscDrop;
}