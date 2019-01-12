using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWeaponTypes : MonoBehaviour {

    public MisileSO[] weapons;

    public MisileSO GetWeapon(int weaponNum)
    {
        var listWeapons = new List<MisileSO>(weapons);
        MisileSO misile = listWeapons.Find(x => x.weaponNum == weaponNum);
        return misile;
    }
    
}
