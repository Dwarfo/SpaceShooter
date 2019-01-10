using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : Move {

    public float speed = 0.1f;
    private float time = 0;
    private GameObject go;

    public override float GetDuration()
    {
        return duration;
    }

    public override void Initialize(GameObject go)
    {
        this.go = go;
    }

    public override void makeAction(float deltaTime)
    {
        go.SetActive(false);
    }

    public override void undoAction()
    {

    }
}
