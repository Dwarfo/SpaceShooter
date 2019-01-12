using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton_MB<Spawner> {

    public LevelStats levelStats;

    [SerializeField]
    private float timeCount = 0;
    private List<GameObject> groups;
    private List<float> activationTime;
    [SerializeField]
    private int currentGroup = 0;
    [SerializeField]
    private float spawningTime = 0;
    [SerializeField]
    private float timeBeforeNewLevelLoad = 5f;
    private bool nextLevel = false;

    private CountSpawningTime countSpawningTime;

    public Events.EmptyEvent OnEnemiesEnded;
    public Events.EmptyEvent OnNextLevel;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        //levelStats = GameManager.Instance.GetCurrentStats();

    }

    void Update ()
    {
        if(countSpawningTime != null && GameManager.Instance.CurrentGameState == GameState.RUNNING)
            countSpawningTime(Time.deltaTime);
	}

    private void InstantiatePacks()
    {
        var stats = levelStats.getDict();
        groups = new List<GameObject>();
        activationTime = new List<float>(stats.Values);

        foreach (GameObject group in stats.Keys)
        {
            GameObject go = Instantiate(group);
            groups.Add(go);
            go.SetActive(false);
        }

        spawningTime = activationTime[currentGroup];
    }

    private void ActivatePack()
    {
        Debug.Log("Activate a group: " + currentGroup);
        if (nextLevel)
        {
            OnNextLevel.Invoke();
            return;
        }

        if (currentGroup == activationTime.Capacity)
        {
            spawningTime = timeBeforeNewLevelLoad;
            currentGroup++;
            return;
        }

        if (currentGroup > activationTime.Capacity)
        {
            Debug.Log("Enemies ended");
            OnEnemiesEnded.Invoke();
            nextLevel = true;
            return;
        }

        groups[currentGroup].SetActive(true);
        spawningTime = activationTime[currentGroup];
        currentGroup++;
    }

    private void Spawn(float current)
    {
        timeCount += Time.deltaTime;
        if (timeCount >= spawningTime)
        {
            ActivatePack();
            timeCount = 0;
        }
    }

    private void HandleGameStateChanged(GameState current, GameState previous)
    {
        if (current == GameState.RUNNING && previous != GameState.PAUSED)
        {
            levelStats = GameManager.Instance.GetCurrentStats();
            currentGroup = 0;
            nextLevel = false;
            InstantiatePacks();
            countSpawningTime += Spawn;
        }

        if (current == GameState.PREGAME)
        {
            countSpawningTime = null;

        }

    }
}

public delegate void CountSpawningTime(float deltaTime);