using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour {

    public int order = 0;
    public bool dontUnsub = false;
    public bool startAnew = false;
    public bool additive = false;
    public bool repeated = false;
    public Transform tr;

    public Events.OnActionFinished onActionFinished;
    public float duration = 2f;

    private void Start()
    {
        
    }

    public abstract void makeAction(float deltaTime);
    public abstract void undoAction();
    public abstract void Initialize(GameObject go);
    public abstract float GetDuration();

}
