using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameElement : MonoBehaviour {

    public Text gameSaveText;
    public Button loadGameButton; 


	// Use this for initialization
	void Start ()
    {
        loadGameButton.onClick.AddListener(HandleLoadGameButton);
	}

    private void HandleLoadGameButton()
    {
        GameManager.Instance.LoadGame(gameSaveText.text);
    }
}
