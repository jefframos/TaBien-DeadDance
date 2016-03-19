using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class WaveModel : MonoBehaviour{
    public List<ActionButtonModel> actionList;    
    public int difficulty;
    public Vector2 grid = new Vector2(5,4);
    public bool firstConf = false;
    public float duration;
    public int totalBeats;
}

