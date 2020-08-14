using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefinitions 
{
    public enum WeaponType
    {
        None = 0,
        Pistol = 1,
        Shotgun = 2,
        Chaingun = 3,
        Sniper = 4
    }

    public enum MiscDropTypes
    {
        None = 0,
        HP = 5
    }

    public enum MovementSoundTypes
    {
        Regular,
        Slime,
        WingFlap
    }
}
