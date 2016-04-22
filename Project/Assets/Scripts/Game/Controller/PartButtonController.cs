using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class PartButtonController : MonoBehaviour {
    public ShopController ShopController;
    public Button Button;
    private Action<PartsModel, PartButtonController> callback;
    public RectTransform Container;
    public CanvasGroup CanvasGroup;
    private PartsModel partsModel;
    public Image ImageThumb;
    public Text LabelValue;
    public GameObject EquipContainer;
    public GameObject PurchaseContainer;
    internal void Build(PartsModel _partModel, Action<PartsModel, PartButtonController> _callback)
    {
        partsModel = _partModel;

        var thumb = Resources.Load<Sprite>(partsModel.BasePath + partsModel.ThumbPath);

        ImageThumb.sprite = thumb;
        LabelValue.text = partsModel.Value.ToString();
        callback = _callback;
        Button.onClick.AddListener(() => {
            Show();
            callback(partsModel, this);
        });

        UpdatePurchaseState();
    }
    internal void UpdatePurchaseState()
    {
        if (partsModel.Purchased)
        {
            EquipContainer.SetActive(true);
            PurchaseContainer.SetActive(false);
        }
        else
        {
            EquipContainer.SetActive(false);
            PurchaseContainer.SetActive(true);
        }
    }
    internal void Show(float delay = 0)
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.DOFade(1, 0.5f).SetDelay(delay);
        Container.DOKill();
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

    internal void UpdateState(PartsModel _partModel)
    {
        partsModel = _partModel;
        LabelValue.text = partsModel.Value.ToString();
        UpdatePurchaseState();
        Show();
    }

    internal void ActiveState()
    {
        EquipContainer.SetActive(false);
        ImageThumb.color = new Color(1, 1, 1, 1);
    }

    internal void UnactiveState()
    {
        EquipContainer.SetActive(true);
        ImageThumb.color = new Color(1, 1, 1, 0.5f);
    }

    internal void UpdateActiveState()
    {
        if (partsModel.Active){
            ActiveState();
        }
        else {
            UnactiveState();
        }
    }
}
