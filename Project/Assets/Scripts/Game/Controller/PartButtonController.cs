using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class PartButtonController : MonoBehaviour {
    public ShopController ShopController;
    public Button Button;
    private Action<PartsModel> callback;
    public RectTransform Container;
    public CanvasGroup CanvasGroup;
    private PartsModel partsModel;
    
    internal void Build(PartsModel _partModel, Action<PartsModel> _callback)
    {
        partsModel = _partModel;
        callback = _callback;
        Button.onClick.AddListener(() => { callback(partsModel); });
    }
    internal void Show(float delay = 0)
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.DOFade(1, 0.5f).SetDelay(delay);
        Container.localPosition = new Vector3(50f,0f,0f);
        Container.DOLocalMoveX(0, 1f).SetEase(Ease.OutElastic).SetDelay(delay);
    }
    internal void Kill(float delay = 0)
    {
        CanvasGroup.DOFade(0, 0.5f).SetDelay(delay);
        Container.DOMoveX(-50, 1).SetDelay(delay).OnComplete(() => { DestroyPart(); });        
    }
    internal void DestroyPart()
    {
        Destroy(gameObject);
    }
}
