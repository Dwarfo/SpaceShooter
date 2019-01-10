using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Move {

    public float speed = 0.1f;
    private float time = 0;

    public enum Side
    {
        Left,Right,Down,Up,LeftUp,LeftDown,RightUp,RightDown
    }

    public Side flySide;

    public override void makeAction(float deltaTime)
    {
        tr.position += GetDirection() * speed;
        time += deltaTime;
        if (time > duration && onActionFinished != null)
            onActionFinished.Invoke(this);
    }

    public override void undoAction()
    {

    }

    public override void Initialize(GameObject go)
    {
        tr = go.GetComponent<Transform>();
        
        Debug.Log("Initialized");
    }

    public override float GetDuration()
    {
        return duration;
    }

    private Vector3 GetDirection()
    {
        switch (flySide) {
            case Side.Left:
                return new Vector2(-1, 0);
            case Side.Right:
                return new Vector2(1, 0);
            case Side.Up:
                return new Vector2(0, 1);
            case Side.Down:
                return new Vector2(0, -1);
            case Side.LeftUp:
                return new Vector2(-0.7f, 0.7f);
            case Side.LeftDown:
                return new Vector2(-0.7f, -0.7f);
            case Side.RightUp:
                return new Vector2(0.7f, 0.7f);
            case Side.RightDown:
                return new Vector2(0.7f, -0.7f);
        }

        return Vector3.zero;
    }
}

