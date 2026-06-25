using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackspeed;

    // Physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    // elemental damage
    public Stat fireDamge;
    public Stat iceDamage;
    public Stat lightningDamage;
}
