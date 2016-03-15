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
    public Image brainImage;
    internal RectTransform rectTarget;
    internal Action endTweenCallback;

    // Update is called once per frame
    void Update () {
	
	}

    internal void SetSpecial()
    {
        brainImage.gameObject.SetActive(true);
        image.gameObject.SetActive(false);

    }
    internal void Build(bool isSpecial, int points)
    {
        if(time > 0.8f)
        {
            time = 0.8f;
        }
        if (isSpecial)
        {
            SetSpecial();
        }
        else {
            brainImage.gameObject.SetActive(false);
            image.gameObject.SetActive(true);
            image.color = color;
        }
        
        this.gameObject.SetActive(true);
        
        imageContainer.localPosition = initPos;
        imageContainer.DOMove(destiny, time).SetDelay(delay).OnComplete(() => {
            Vector2 scl = new Vector2(1, 1);
            rectTarget.localScale = scl;
            rectTarget.DOScale(new Vector2(1.5f, 1.5f), 0.5f).From();
            if (endTweenCallback != null)
            {
                endTweenCallback();
            }
            Destroy(this.gameObject);
        });
        imageContainer.DOScale(new Vector3(0.5f, 0.5f, 1f), time).SetDelay(delay);        
    }
}
