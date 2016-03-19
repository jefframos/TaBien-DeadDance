using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

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
        myScript.totalBeats = 20;
    }
    override public void OnInspectorGUI()
    {
        var myScript = target as WaveModel;

        ActionButtonModel[] tempList = myScript.GetComponentsInChildren<ActionButtonModel>();
        myScript.actionList = new List<ActionButtonModel>();
        foreach (ActionButtonModel item in tempList)
        {
            myScript.actionList.Add(item);
        }

        myScript.difficulty = EditorGUILayout.IntField("Difficulty", myScript.difficulty);
        myScript.grid = EditorGUILayout.Vector2Field("Grid", myScript.grid);
        myScript.totalBeats = EditorGUILayout.IntField("Total Beats", myScript.totalBeats);


        EditorUtility.SetDirty(myScript);
    }
}
