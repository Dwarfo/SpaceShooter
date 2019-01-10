using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelEnded : MonoBehaviour {

    public Text levelClearedText;
	// Use this for initialization
	void Start ()
    {
        Spawner.Instance.OnEnemiesEnded.AddListener(HandleEnemiesEnded);
        if (levelClearedText != null)
            levelClearedText.gameObject.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleEnemiesEnded()
    {
        levelClearedText.gameObject.SetActive(true);
    }


}
