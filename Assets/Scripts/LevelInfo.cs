using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public string lvlName;
    public bool success;
    public bool[] fragments;
    public float time;

    public void Reset()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            fragments[i] = false;
        }
        time = 0;
        success = false;
    }
}
