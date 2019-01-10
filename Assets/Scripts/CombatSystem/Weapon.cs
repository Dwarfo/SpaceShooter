using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public MisileSO currentWeapon;
    [Header("Custom cooldown")]
    [Tooltip("If Value equals -1, default cooldown value will be used")]
    public float coolDown = -1;
    public Events.FloatEvent onCoolDownStarted;
    public bool isNotAtCooldown = true;

    private AudioSource audioSource;
    private Faction shooterFaction;

    private void Start()
    {
        shooterFaction = gameObject.GetComponent<Stats>()._Faction;
        if (audioSource == null)
            audioSource = gameObject.GetComponent<AudioSource>();
        if (coolDown == -1)
            coolDown = currentWeapon.coolDown;
    }

    public virtual void FireProjectile(Transform firePos)
    {
        if (isNotAtCooldown)
        {
            GameObject go = Instantiate(currentWeapon.misile, firePos.position, firePos.rotation);
            go.GetComponent<DamageInfo>().toIgnore = shooterFaction;
            ResetCooldown();
            onCoolDownStarted.Invoke(coolDown);
            Invoke("ResetCooldown", coolDown);

            int index = Random.Range(0, currentWeapon.shootingAudio.Length - 1);
            //Debug.Log("Index " + index + " Length " + currentWeapon.shootingAudio.Length);
            audioSource.PlayOneShot(currentWeapon.shootingAudio[index]);

        }
    }

    private void ResetCooldown()
    {
        isNotAtCooldown = !isNotAtCooldown;
    }

    public virtual void EquipWeapon(MisileSO weapon)
    {
        currentWeapon = weapon;
    }
}
