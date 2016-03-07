using UnityEngine;
using System.Collections;
using UnityEditor;

public class ActionButtonModel : MonoBehaviour{
    [SerializeField]
    public float timeToShow = 1;
    public Color color = new Color();
    public Vector2 gridPosition = new Vector2();
    public int order = 0;
    public bool placed = false;
}

[CustomEditor(typeof(ActionButtonModel))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as ActionButtonModel;

        //myScript.order = 10;
        //myScript.gridPosition.x = myScript.transform.position.x;
        myScript.timeToShow = EditorGUILayout.FloatField(1);
        Vector2 tempVector = myScript.transform.position;


        Vector2 tempV3 = new Vector2();
        RectTransform actionArea = myScript.transform.parent.GetComponent<RectTransform>();
        int rndX = (int)(myScript.transform.localPosition.x - actionArea.rect.min.x);// UnityEngine.Random.Range(0, (int)grid.x);
        int rndY = (int)(myScript.transform.localPosition.y - actionArea.rect.min.y);//UnityEngine.Random.Range(0, (int)grid.y);

        float tempX = (actionArea.rect.max.x - actionArea.rect.min.x) / (GameController.grid.x + 1);
        float tempY = (actionArea.rect.max.y - actionArea.rect.min.y) / (GameController.grid.y+1);

        tempV3.x = Mathf.Floor(rndX / tempX);
        tempV3.y = Mathf.Floor(rndY / tempY);

        //Debug.Log(rndX);
        myScript.gridPosition = EditorGUILayout.Vector2Field("GridPos", tempV3);
        myScript.order = EditorGUILayout.IntField(0);
        myScript.color = EditorGUILayout.ColorField(new Color());
        //if (myScript.flag)
        //    myScript.i = EditorGUILayout.IntSlider("I field:", myScript.i, 1, 100);

    }
}

