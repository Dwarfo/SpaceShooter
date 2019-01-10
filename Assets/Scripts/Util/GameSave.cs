using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave {

    public float maxHp;
    public float armor;
    public float currentHp;
    public float speed;
    public float rotSpeed;
    public float drag;
    public int LivesRemaining;
    public int currentLevelNum;
    public MisileSO currentWeapon;
}
