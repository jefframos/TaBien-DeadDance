using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class LifeHearthView : MonoBehaviour
{
    public RectTransform filled;
    public RectTransform unfilled;
    // Use this for initialization
    void Start () {
        

    }
	
    public void Reset()
    {
        unfilled.gameObject.SetActive(false);
        filled.gameObject.SetActive(true);
        filled.DOScale(1, 0f);
        unfilled.DOScale(1, 0f);
    }
    public void Fill()
    {
        unfilled.DOScale(0, 0.5f);
        filled.gameObject.SetActive(true);
        filled.transform.localScale = new Vector3();
        filled.DOScale(1, 0.9f).SetEase(Ease.OutElastic).OnComplete(() => { unfilled.gameObject.SetActive(false); });
    }
    public void Unfill()
    {
        filled.DOScale(0, 0.5f);
        unfilled.gameObject.SetActive(true);
        unfilled.transform.localScale = new Vector3();
        unfilled.DOScale(1, 0.9f).SetEase(Ease.OutElastic).OnComplete(() => { filled.gameObject.SetActive(false); });

    }
}
