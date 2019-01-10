using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConsistentStats", menuName = "Player/ConsistentStats", order = 1)]
public class ConsistentStats : ScriptableObject
{

    public int LivesRemaining;
    public float maxHp;
    public float armor;
    public float currentHp;
    public float speed;
    public float rotSpeed;
    public float drag;
    public MisileSO currentWeapon;
    public Vector3 startingPosition;

    public void ReadStats(PlayerStats stats, Weapon weapon)
    {
        LivesRemaining = stats.LivesRemaining;
        maxHp = stats.maxHp;
        armor = stats.armor;
        currentHp = stats.currentHp;
        speed = stats.speed;
        rotSpeed = stats.rotSpeed;
        drag = stats.drag;
        currentWeapon = weapon.currentWeapon;
    }

    public void WriteStats(PlayerStats stats, Weapon weapon)
    {
        stats.LivesRemaining = LivesRemaining;
        stats.maxHp = maxHp;
        stats.armor = armor;
        stats.currentHp = currentHp;
        stats.speed = speed;
        stats.rotSpeed = rotSpeed;
        stats.drag = drag;
        weapon.currentWeapon = currentWeapon;
        stats.transform.position = startingPosition;
    }

}