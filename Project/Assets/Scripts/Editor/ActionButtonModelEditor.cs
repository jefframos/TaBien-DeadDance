using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

[CustomEditor(typeof(ActionButtonModel))]
public class ActionButtonModelEditor : Editor
{
    override public void OnInspectorGUI()
    {
       

        var myScript = target as ActionButtonModel;



        Vector2 tempV3 = new Vector2();
        RectTransform actionArea = myScript.transform.parent.GetComponent<RectTransform>();
        WaveModel waveModel = myScript.transform.parent.GetComponentInParent<WaveModel>();
        Image image = myScript.transform.GetComponent<Image>();

        myScript.color = image.color;
        EditorGUILayout.ColorField("Color", image.color);
        //myScript.color = EditorGUILayout.ColorField("New Color", myScript.color);


        int rndX = (int)(myScript.transform.localPosition.x - actionArea.rect.min.x);
        int rndY = (int)(myScript.transform.localPosition.y - actionArea.rect.min.y);

        float tempX = (actionArea.rect.max.x - actionArea.rect.min.x) / (waveModel.grid.x + 1);
        float tempY = (actionArea.rect.max.y - actionArea.rect.min.y) / (waveModel.grid.y + 1);

        tempV3.x = Mathf.Floor(rndX / tempX);
        tempV3.y = Mathf.Floor(rndY / tempY);

        myScript.gridPosition = EditorGUILayout.Vector2Field("GridPos", tempV3);

        // myScript.timeToShow = EditorGUILayout.FloatField("Time", myScript.timeToShow);
        myScript.order = EditorGUILayout.IntField("Order", myScript.order);

        EditorGUILayout.LabelField("Beat: " + ((float)myScript.quarterBeatAppear / 4).ToString());
        myScript.quarterBeatAppear = EditorGUILayout.IntSlider(myScript.quarterBeatAppear, 0, waveModel.totalBeats * 4);


        EditorGUILayout.LabelField("Beat to show: " + ((float)myScript.quarterBeatToTap / 4).ToString());
        myScript.quarterBeatToTap = EditorGUILayout.IntSlider(myScript.quarterBeatToTap, 0, waveModel.totalBeats * 4);


        EditorGUILayout.LabelField("Will appear on Beat: " + (((float)myScript.quarterBeatAppear / 4) - (float)myScript.quarterBeatToTap / 4).ToString());

        //  myScript.timeToTap = EditorGUILayout.FloatField("Time to tap", myScript.timeToTap);
        myScript.maxScale = EditorGUILayout.FloatField("Max Scale", myScript.maxScale);

        myScript.behaviour = (BehaviourType)EditorGUILayout.EnumPopup(myScript.behaviour);
        myScript.actionType = (ActionType)EditorGUILayout.EnumPopup(myScript.actionType);


        
        DrawDefaultInspector();
        EditorUtility.SetDirty(myScript);

    }
}
