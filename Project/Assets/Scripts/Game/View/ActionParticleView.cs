using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;

public class ActionParticleView : MonoBehaviour {
    public Color color;
    public int quant = 3;
    public Vector3 destiny;
    public Vector3 initPos;
    public float delay;
    public float time = 2f;
    public RectTransform imageContainer;
    public Image image;
    internal RectTransform rectTarget;

    // Update is called once per frame
    void Update () {
	
	}

    internal void Build()
    {
        if(time > 0.8f)
        {
            time = 0.8f;
        }
        this.gameObject.SetActive(true);
        image.color = color;
        imageContainer.localPosition = initPos;
        imageContainer.DOMove(destiny, time).SetDelay(delay).OnComplete(() => {
            Vector2 scl = new Vector2(1, 1);
            rectTarget.localScale = scl;
            rectTarget.DOScale(new Vector2(1.5f, 1.5f), 0.5f).From();
            Destroy(this.gameObject);
        });
        imageContainer.DOScale(new Vector3(0.5f, 0.5f, 1f), time).SetDelay(delay);        
    }
}
