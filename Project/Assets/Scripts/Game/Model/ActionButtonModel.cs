using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class ActionButtonModel : MonoBehaviour{
    
    public BehaviourType behaviour = BehaviourType.SCALE;
    public ActionType actionType = ActionType.NORMAL;
    //public float timeToShow = 1;
    public Color color = new Color();
    public Vector2 gridPosition = new Vector2();
    public int order = 0;
    public bool placed = false;
    //public float timeToTap = 3;
    public float maxScale = 5;
    public int quarterBeatAppear = 20;
    public int quarterBeatToTap = 4;

    public int frameBeatAppear = 20;
    public int frameBeatToTap = 4;
    public AudioClip beatSound;
    public AudioClip wrongSound;

    public static ActionButtonModel GetActionButtonModel()
    {
        ActionButtonModel action = new ActionButtonModel();



        return action;
    }
}