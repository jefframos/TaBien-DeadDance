using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemSlotController : MonoBehaviour {
    public ItemSlotModel ItemSlotModel;
    public Text LabelInfo;
    public Button Button;
    public Image Image;
    public Action<ItemSlotModel> ClickCallback;
    public RectTransform RectPlus;
    // Use this for initialization
    public void Build(ItemSlotModel itemSlotModel, Action<ItemSlotModel> clickCallback)
    {
        ItemSlotModel = itemSlotModel;
        ClickCallback = clickCallback;

        LabelInfo.text = ItemSlotModel.ItemModel.Name;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => {
            ClickCallback(ItemSlotModel);
        });
    }
    public void InGameMode()
    {
        Button.enabled = false;
        RectPlus.gameObject.SetActive(false);
    }
    public void OutGameMode()
    {
        Button.enabled = true;
        RectPlus.gameObject.SetActive(true);
    }
}
