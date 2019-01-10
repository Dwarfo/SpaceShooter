using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour {

    public List<Move> actions = new List<Move>();

    private void Awake()
    {
        var acts = gameObject.GetComponents<Move>();

        foreach (Move action in acts)
            actions.Add(action);

        actions.Sort((x, y) => x.order.CompareTo(y.order));
    }
}
