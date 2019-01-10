using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour {

    [SerializeField]
    private List<Move> actions;
    private MakeMove action;
    private int actionNum = 0;
    private bool gamePaused = false;

    public Events.FloatEvent OnEnemyDeath;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        actions = transform.GetComponentInChildren<ActionList>().actions;
        foreach (var act in actions)
        {
            act.Initialize(gameObject);
            act.onActionFinished.AddListener(HandleOnActionFinished);
        }
        action += actions[actionNum].makeAction;
    }


    void Update()
    {
        if (gamePaused)
            return;
        if (action != null)
            action(Time.deltaTime);
    }

    private void HandleOnActionFinished(Move thatMove)
    {
        Debug.Log("Action finished");

        if (!thatMove.dontUnsub)
            action -= thatMove.makeAction;

        if (actionNum < actions.Count - 1)
        {
            actionNum++;
            action += actions[actionNum].makeAction;

            if (actions[actionNum].additive && (actionNum + 1) <= actions.Count - 1)
            {
                actionNum++;
                action += actions[actionNum].makeAction;
            }
        }

    }

    private void HandleGameStateChanged(GameState current, GameState previous)
    {
        if (current == GameState.PAUSED)
            gamePaused = true;
        else
            gamePaused = false;
    }

}


public delegate void MakeMove(float deltaTime);
