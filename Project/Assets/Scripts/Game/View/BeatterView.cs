using UnityEngine;
using System.Collections;
using DG.Tweening;
public class BeatterView : MonoBehaviour {
    public RectTransform graphic;
    public float maxScale = 2f;
    public float minScale = 1f;
    public float currentScale = 1f;
    public float speed = 2f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (currentScale < minScale)
        {
            currentScale = minScale;
        }
        else
        {
            currentScale -= speed * Time.deltaTime;
        }
        graphic.DOScale(currentScale, 0);
    }
    public void Beat()
    {
        currentScale = maxScale;
    }
}
