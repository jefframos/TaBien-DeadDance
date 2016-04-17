using UnityEngine;
using System.Collections;
using DG.Tweening;
public class BeatterView : MonoBehaviour {
    public RectTransform graphic;
    public float maxScale = 2f;
    public float minScale = 1f;
    public float currentScale = 1f;
    public float speed = 2f;
    public bool Running;
	// Use this for initialization
	void Start () {
        currentScale = maxScale;
        Running = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Running)
        {
            return;
        }
        graphic.DOScale(currentScale, 0);
        if (currentScale < minScale)
        {
            currentScale = minScale;
        }
        else
        {
            currentScale -= speed * Time.deltaTime;
        }
        
    }
    public void Beat()
    {
        currentScale = maxScale;
    }
}
