using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour, IMenu {

    public Button continueButton;
    public Button saveGameButton;
    public Button mainMenuButton;
    public Button quitGameButton;

    public Text statsText;

    void Start ()
    {
        continueButton.onClick.AddListener(HandleContinueButton);
        saveGameButton.onClick.AddListener(HandleSaveGameButton);
        mainMenuButton.onClick.AddListener(HandleMainMenuButton);
        quitGameButton.onClick.AddListener(HandleQuitGameButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleContinueButton()
    {
        GameManager.Instance.StartGame();
    }

    private void HandleSaveGameButton()
    {
        Debug.Log(Application.persistentDataPath);
        UIManager.Instance.ToSaveMenu(this);
    }

    private void HandleMainMenuButton()
    {
        //GameManager.Instance.RestartGame();
        UIManager.Instance.ToMainMenu(this);
        GameManager.Instance.GoToMainMenu();
    }

    private void HandleQuitGameButton()
    {
        GameManager.Instance.QuitGame();
    }


    public void SetMenuActive(bool active)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(active);
        List<Transform> listedTransforms = new List<Transform>(transforms);

        listedTransforms.Remove(gameObject.transform);

        foreach (Transform tr in listedTransforms)
            tr.gameObject.SetActive(active);
    }

}
