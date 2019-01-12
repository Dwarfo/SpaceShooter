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
    [SerializeField]
    private LoadMenu loadMenu;
    [SerializeField]
    private StatsMenu statsMenu;
    [SerializeField]
    private SaveMenu saveMenu;


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
        if (previousState == GameState.PAUSED && currentState == GameState.RUNNING)
        {
            pauseMenu.SetMenuActive(false);
        }

        if (previousState == GameState.RUNNING && currentState == GameState.PAUSED)
        {
            pauseMenu.SetMenuActive(true);
        }

        if (previousState == GameState.PREGAME && currentState == GameState.RUNNING)
        {
            mainMenu.SetMenuActive(false);
        }

        if (previousState != GameState.PREGAME && currentState == GameState.PREGAME)
        {
            mainMenu.SetMenuActive(true);
        }

        if (previousState == GameState.RUNNING && currentState == GameState.LEVELSTATS)
        {
            statsMenu.SetMenuActive(true);
        }

        if (previousState == GameState.LEVELSTATS && currentState == GameState.RUNNING)
        {
            statsMenu.SetMenuActive(false);
        }

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

    private void TransitionInMenus(IMenu from, IMenu to)
    {
        from.SetMenuActive(false);
        to.SetMenuActive(true);
    }

    public void ToLoadMenu()
    {
        TransitionInMenus(mainMenu, loadMenu);
    }

    public void ToStatsMenu(IMenu from)
    {
        TransitionInMenus(from,statsMenu);
    }

    public void ToPauseMenu(IMenu from)
    {
        TransitionInMenus(from, pauseMenu);
    }

    public void ToMainMenu(IMenu from)
    {
        TransitionInMenus(from, mainMenu);
    }

    public void ToSaveMenu(IMenu from)
    {
        TransitionInMenus(from, saveMenu);
    }
}
