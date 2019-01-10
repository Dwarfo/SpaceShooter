using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewLevelStats", menuName = "Level/LevelStats", order = 1)]
public class LevelStats : ScriptableObject {

    //public Dictionary<GameObject, float> groupsAndTime;
    public GameObject[] groups;
    public float[] time;

    public Dictionary<GameObject, float> getDict()
    {
        var dict = new Dictionary<GameObject, float>();
        int i = 0;
        foreach (GameObject group in groups)
        {
            dict.Add(group, time[i]);
            i++;
        }

        return dict;
    }
}
