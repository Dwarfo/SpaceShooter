using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAndShoot : Move {

    public float cooldownBetweenSeries;
    public int shotSeries = 4;

    private bool playerIsAlive = true;
    private float time = 0;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Transform firepos;

    [SerializeField]
    private int shotsFired = 0;
    private float currentCd = 0;


    public override void makeAction(float deltaTime)
    {

        if (!playerIsAlive)
            return;

        RotateTowards(tr, target);

        if (shotsFired <= shotSeries)
        {
            weapon.FireProjectile(firepos);
        }

        if (shotsFired >= shotSeries)
        {
            currentCd += deltaTime;
            if (currentCd > cooldownBetweenSeries)
            {
                currentCd = 0;
                shotsFired = 0;
            }
        }

        time += deltaTime;
        if (time > duration && onActionFinished != null)
            onActionFinished.Invoke(this);

    }

    public override void undoAction()
    {

    }

    public override void Initialize(GameObject go)
    {
        tr = go.GetComponent<Transform>();
        target = GameManager.Instance.player.transform;
        weapon = go.GetComponent<Weapon>();

        GameManager.Instance.player.GetComponent<PlayerStats>().onDestroy.AddListener(HandlePlayerDeath);
        GameManager.Instance.OnPlayerResurected.AddListener(HandlePlayerResurected);
        weapon.onCoolDownStarted.AddListener(HandleOnCooldownStarted);

        firepos = gameObject.GetComponentInChildren<Transform>();
        //this.dontUnsub = true;
        //this.additive = true;
        //this.repeated = true;
        //this.duration = 2f;
        Debug.Log("Initialized");
    }

    public override float GetDuration()
    {
        return duration;
    }

    public void RotateTowards(Transform character, Transform direction)
    {
        Vector3 difference = direction.position - character.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
        character.rotation = Quaternion.Slerp(character.rotation, Quaternion.Euler(0f, 0f, rotation_z), Time.deltaTime * 5f);
    }

    private void HandleOnCooldownStarted(float cooldown)
    {
        shotsFired++;
    }

    private void HandlePlayerDeath(GameObject player)
    {
        playerIsAlive = false;
    }

    private void HandlePlayerResurected()
    {
        playerIsAlive = true;
    }
}
