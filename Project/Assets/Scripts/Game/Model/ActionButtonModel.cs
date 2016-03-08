using UnityEngine;
using System.Collections;
using UnityEditor;

public class ActionButtonModel : MonoBehaviour{
    public float timeToShow = 1;
    public Color color = new Color();
    public Vector2 gridPosition = new Vector2();
    public int order = 0;
    public bool placed = false;
    public float timeToTap = 3;
    public float maxScale = 5;
}

[CustomEditor(typeof(ActionButtonModel))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as ActionButtonModel;
        
        Vector2 tempVector = myScript.transform.position;

        Vector2 tempV3 = new Vector2();
        RectTransform actionArea = myScript.transform.parent.GetComponent<RectTransform>();
        WaveModel waveModel = myScript.transform.parent.GetComponentInParent<WaveModel>();
        int rndX = (int)(myScript.transform.localPosition.x - actionArea.rect.min.x);
        int rndY = (int)(myScript.transform.localPosition.y - actionArea.rect.min.y);

        float tempX = (actionArea.rect.max.x - actionArea.rect.min.x) / (waveModel.grid.x + 1);
        float tempY = (actionArea.rect.max.y - actionArea.rect.min.y) / (waveModel.grid.y + 1);

        tempV3.x = Mathf.Floor(rndX / tempX);
        tempV3.y = Mathf.Floor(rndY / tempY); 
               
        myScript.gridPosition = EditorGUILayout.Vector2Field("GridPos", tempV3);

        myScript.timeToShow = EditorGUILayout.FloatField("Time", myScript.timeToShow);
        myScript.timeToTap = EditorGUILayout.FloatField("Time to tap", myScript.timeToTap);
        myScript.maxScale = EditorGUILayout.FloatField("Max Scale", myScript.maxScale);
        myScript.order = EditorGUILayout.IntField("Order", myScript.order);
        myScript.color = EditorGUILayout.ColorField("Color", myScript.color);
        

        EditorUtility.SetDirty(myScript);

    }
}

