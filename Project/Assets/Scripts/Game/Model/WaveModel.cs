using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class WaveModel : MonoBehaviour{
    public ActionButtonModel[] actionList;    
    public int difficulty;
    public Vector2 grid = new Vector2(5,4);
    public bool firstConf = false;
    public float duration;
}
[CustomEditor(typeof(WaveModel))]
public class CatchActions : Editor
{
    void OnEnable()
    {
        var myScript = target as WaveModel;
        if (myScript.firstConf)
        {
            return;
        }
        myScript.grid = new Vector2(5, 4);
        myScript.firstConf = true;
        myScript.duration = 5;
    }
    override public void OnInspectorGUI()
    {
        var myScript = target as WaveModel;
        myScript.actionList = myScript.GetComponentsInChildren<ActionButtonModel>();

        myScript.difficulty = EditorGUILayout.IntField("Difficulty", myScript.difficulty);
        myScript.grid = EditorGUILayout.Vector2Field("Grid", myScript.grid);
        myScript.duration = EditorGUILayout.FloatField("Duration", myScript.duration);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScript);
        }
    }
}

