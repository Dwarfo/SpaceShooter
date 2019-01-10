using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IMenu {

    public Button startGameButton;
    public Button loadGameButton;
    public Button quitGameButton;

    // Use this for initialization
    void Start()
    {
        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
        loadGameButton.onClick.AddListener(ShowLoadMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleGameStateChanged(GameState currentState, GameState previousState)
    {
        if (previousState == GameState.PREGAME && currentState == GameState.RUNNING)
        {
            SetMenuActive(false);
        }

        if (previousState != GameState.PREGAME && currentState ==GameState.PREGAME)
        {
            SetMenuActive(true);
        }
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    private void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }


    private void ShowLoadMenu()
    {
        UIManager.Instance.ToLoadMenu();
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
