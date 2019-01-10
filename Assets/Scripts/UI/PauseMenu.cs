using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour,IMenu {

    public Button continueButton;
    public Button saveGameButton;
    public Button mainMenuButton;
    public Button quitGameButton;

    private void Start()
    {
        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        UIManager.Instance.SetPauseMenu(this);

        continueButton.onClick.AddListener(HandleContinueButton);
        saveGameButton.onClick.AddListener(HandleSaveGameButton);
        mainMenuButton.onClick.AddListener(HandleMainMenuButton);
        quitGameButton.onClick.AddListener(HandleQuitGameButton);

        //gameObject.SetActive(false);
        SetMenuActive(false);
    }

    private void HandleContinueButton()
    {
        GameManager.Instance.UpdateState(GameState.RUNNING);
    }

    private void HandleSaveGameButton()
    {
        Debug.Log(Application.persistentDataPath);
        GameManager.Instance.SaveGame("228");
    }

    private void HandleMainMenuButton()
    {
        GameManager.Instance.RestartGame();
    }

    private void HandleQuitGameButton()
    {
        GameManager.Instance.QuitGame();
    }

    private void HandleGameStateChanged(GameState previous, GameState current)
    {
        if (previous == GameState.PAUSED && current == GameState.RUNNING)
            SetMenuActive(true);
        if (previous == GameState.RUNNING && current == GameState.PAUSED)
            SetMenuActive(false);
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
