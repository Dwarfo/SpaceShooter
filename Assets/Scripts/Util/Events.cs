using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events {

    [System.Serializable]
    public class EmptyEvent : UnityEvent { }

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [System.Serializable]
    public class EventGameState : UnityEvent<GameState, GameState> { }

    [System.Serializable]
    public class EventFadeComplete : UnityEvent<bool> { }

    [System.Serializable]
    public class OnDestroy : UnityEvent<GameObject> { }

    [System.Serializable]
    public class OnPlayerReady : UnityEvent<GameObject> { }

    [System.Serializable]
    public class OnPlayerDamageReceived : UnityEvent<float> { }

    [System.Serializable]
    public class OnBackGroundRan : UnityEvent<Transform, float> { }

    [System.Serializable]
    public class OnActionFinished : UnityEvent<Move> { }

}
