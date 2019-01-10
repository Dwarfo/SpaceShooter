using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour {

    public Text lifeCount;

    public Image weaponIcon;
    public Text weaponName;

    private void Start()
    {
        GameManager.Instance.player.GetComponent<PlayerStats>().onDestroy.AddListener(HandlePlayerDeath);
        UIManager.Instance.SetInterface(gameObject);
        HandleWeaponChange(GameManager.Instance.player);
        HandlePlayerDeath(gameObject);
    }


    public void HandlePlayerDeath(GameObject player)
    {
        int lifeRemaining = GameManager.Instance.playerConsistentStats.LivesRemaining;
        lifeCount.text = "X " + lifeRemaining.ToString();
    }

    private void HandleWeaponChange(GameObject player)
    {
        weaponIcon.sprite = player.GetComponent<Weapon>().currentWeapon.weaponIcon;
        weaponName.text = player.GetComponent<Weapon>().currentWeapon.weaponName;

    }
}
