using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton_MB<UIManager>
{

    //public MainMenu mainMenu;
    [SerializeField]
    private Camera dummyCamera;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject playerInterface;
    [SerializeField]
    private MainMenu mainMenu;



    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleStartGame);
    }

    private void Update()
    {

    }

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }

    private void HandleGameStateChanged(GameState currentState, GameState previousState)
    {
        if (previousState == GameState.PREGAME)
            return;
        if(pauseMenu != null)
            pauseMenu.gameObject.SetActive(currentState == GameState.PAUSED);

    }

    private void HandleStartGame(GameState currentState, GameState previousState)
    {
        if (previousState == GameState.PREGAME && currentState == GameState.RUNNING)
        {
            //miniMap.SetActive(true);
        }

    }

    public void SetPauseMenu(PauseMenu pm)
    {
        this.pauseMenu = pm;
    }

    public void SetInterface(GameObject pi)
    {
        this.playerInterface = pi;
    }
}
