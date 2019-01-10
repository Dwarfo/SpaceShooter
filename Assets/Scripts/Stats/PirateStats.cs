using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateStats : Stats {

    public GameObject explosion;

    public override void GetDestroyed(GameObject obj)
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        base.GetDestroyed(obj);
        Destroy(gameObject);
    }
}
