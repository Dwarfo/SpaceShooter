using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour {

    public float damage;
    public Faction toIgnore;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (toIgnore == Faction.IGNOREMISILES)
            return;
        if (collision.gameObject.GetComponent<Stats>() != null && collision.gameObject.GetComponent<Stats>()._Faction != toIgnore)
            collision.gameObject.GetComponent<Stats>().DecreaseHp(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (toIgnore == Faction.IGNOREMISILES)
            return;
        if (collision.gameObject.GetComponent<Stats>() != null && collision.gameObject.GetComponent<Stats>()._Faction != toIgnore)
            collision.gameObject.GetComponent<Stats>().DecreaseHp(damage);
    }
}
