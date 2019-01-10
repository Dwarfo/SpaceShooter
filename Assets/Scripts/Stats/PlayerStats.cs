using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats {

    public int LivesRemaining = 3;
    public Events.OnPlayerDamageReceived onHpChanged;
    public GameObject explosion;

    private void Start()
    {
        GameManager.Instance.playerConsistentStats.WriteStats(this, gameObject.GetComponent<Weapon>());
        GameManager.Instance.player = gameObject;
        onDestroy.AddListener(GameManager.Instance.HandlePlayerDeath);
    }

    public override void IncreaseHp(float amount)
    {
        base.IncreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }

    public override void DecreaseHp(float amount)
    {
        base.DecreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }

    public override void GetDestroyed(GameObject player)
    {
        LivesRemaining--;
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        base.GetDestroyed(player);
        gameObject.SetActive(false);
    }
}
