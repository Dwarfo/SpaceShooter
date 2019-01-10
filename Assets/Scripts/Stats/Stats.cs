using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    public float maxHp;
    public float armor;
    public float currentHp;
    public float speed;
    public float rotSpeed;
    public float drag;
    public Events.OnDestroy onDestroy;

    [SerializeField]
    private Faction faction;

    public Faction _Faction
    {
        get { return faction; }
    }

    public virtual void DecreaseHp(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
            GetDestroyed(gameObject);
    }

    public virtual void IncreaseHp(float amount)
    {
        currentHp += amount;
        if (currentHp >= maxHp)
            currentHp = maxHp;
    }

    public virtual void GetDestroyed(GameObject player)
    {
        onDestroy.Invoke(player);
    }
}

public enum Faction
{
    PLAYER,
    PIRATE,
    SPACEOBJECT,
    IGNOREMISILES
}
