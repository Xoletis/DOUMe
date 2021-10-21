using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "My Game/Weapon")]
public class WeaponStats : ScriptableObject
{
    //attributes
    public int wpnDmg = 1;
    public float fireRate = 0.25f;
    public float wpnRange = 200f;
    public int munitions = 0;
    public int maxMunitions = 50;
}
