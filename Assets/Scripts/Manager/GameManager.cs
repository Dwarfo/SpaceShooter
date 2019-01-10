using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : Singleton_MB<GameManager> {

    public Camera dummyCamera;
    public GameObject player;
    public ConsistentStats playerConsistentStats;
    
    public LevelStats[] levels;
    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;
    public Events.EmptyEvent OnPlayerResurected;
    public Events.EmptyEvent OnGameWon;

    [SerializeField]
    private int currentLevelStats; 
    private List<GameObject> instancedSystemPrefabs;
    [SerializeField]
    private string currentLevelName = string.Empty;
    [SerializeField]
    private GameState currentGameState = GameState.PREGAME;

    private List<AsyncOperation> loadOperations;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }


    void Start()
    {
        currentLevelStats = 0;
        DontDestroyOnLoad(gameObject);
        instancedSystemPrefabs = new List<GameObject>();
        loadOperations = new List<AsyncOperation>();
        Spawner.Instance.OnNextLevel.AddListener(HandleNextLevel);

        InstantiateSystemPrefabs();
    }

    void Update()
    {

    }

    public void StartGame()
    {
        LoadLevel("GameScene");
    }

    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);

    }

    public void RestartGame()
    {
        UnloadLevel(currentLevelName);
        //UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        #else

        Application.Quit();

        #endif
    }

    public void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;
            default:
                break;
        }

        OnGameStateChanged.Invoke(currentGameState, previousGameState);
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevelName));

            if (loadOperations.Count == 0)
            {
                Debug.Log("RUNNING!!!!!!!!!!!!!!!!!!!!!!!");
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete");
        
    }

    private void OnUnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            Debug.Log("CONTAINS");
            loadOperations.Remove(ao);

            if (loadOperations.Count == 0)
            {
                UpdateState(GameState.PREGAME);
                Debug.Log("Unload Complete");
            }
        }

        //Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        loadOperations.Add(ao);

        ao.completed += OnLoadOperationComplete;
        ao.allowSceneActivation = true;
        currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        loadOperations.Add(ao);
        ao.completed += OnUnLoadOperationComplete;
        currentLevelName = "BootScene";
    }

    public void HandlePlayerDeath(GameObject player)
    {
        playerConsistentStats.LivesRemaining--;
        playerConsistentStats.WriteStats(player.GetComponent<PlayerStats>(), player.GetComponent<Weapon>());

        Invoke("ResurectPlayer", 2);
    }

    private void HandleNextLevel()
    {
        if (currentLevelStats == levels.Length - 1)
        {
            OnGameWon.Invoke();
            Debug.Log("Game Won");
            return;
        }

        currentLevelStats++;
        Debug.Log("Now loading new level");
        UnloadLevel(currentLevelName);
        UpdateState(GameState.PREGAME);
        StartGame();
    }

    private void ResurectPlayer()
    {
        player.SetActive(true);
        OnPlayerResurected.Invoke();
    }

    public LevelStats GetCurrentStats()
    {
        return levels[currentLevelStats];
    }

    private GameSave GetGameSave()
    {
        var playerStats = player.GetComponent<PlayerStats>();

        GameSave gameSave = new GameSave();
        gameSave.armor = playerStats.armor;
        gameSave.currentHp = playerStats.currentHp;
        gameSave.drag = playerStats.drag;
        gameSave.LivesRemaining = playerStats.LivesRemaining;
        gameSave.maxHp = playerStats.maxHp;
        gameSave.rotSpeed = playerStats.rotSpeed;
        gameSave.speed = playerStats.speed;
        gameSave.currentLevelNum = currentLevelStats;
        gameSave.currentWeapon = player.GetComponent<Weapon>().currentWeapon;

        return gameSave;
    }

    public void SaveGame(String savename)
    {
        BinaryFormatter gameSaver = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + savename + ".sv", FileMode.OpenOrCreate);

        GameSave gameSave = GetGameSave();

        gameSaver.Serialize(file, gameSave);
        file.Close();
    }
    /*
    public void LoadGame()
    {

    }
    */
}

public enum GameState
{
    PREGAME,
    RUNNING,
    PAUSED
}