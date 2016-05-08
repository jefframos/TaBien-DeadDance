using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EquipableItemPopUpController : MonoBehaviour {
    public Animator Animator;
    public RectTransform RectCurrentItemContainer;
    public RectTransform RectItemListContainer;
    public GameObject PrefabItemShop;
    public List<ItemShopController> ListItens;
    public ItemShopController CurrentListItem;
    private bool built;
    // Use this for initialization
    public void Build () {
        if (built)
        {
            return;
        }
        built = true;
        ListItens = new List<ItemShopController>();
        for (int i = 0; i < InventoryManager.ListEquipableItemData.Count; i++)
        {
            AddItemOnList(InventoryManager.ListEquipableItemData[i]);
        }
        AddItemOnCurrent(InventoryManager.ListEquipableItemData[0]);
    }

    private void AddItemOnList(ItemModel itemModel)
    {
        GameObject itemOnList = Instantiate(PrefabItemShop);
        itemOnList.transform.SetParent(RectItemListContainer, false);
        ItemShopController itemShopController = itemOnList.GetComponent<ItemShopController>();
        itemShopController.Build(itemModel);
        ListItens.Add(itemShopController);
    }

    private void AddItemOnCurrent(ItemModel itemModel)
    {
        GameObject itemOnList = Instantiate(PrefabItemShop);
        itemOnList.transform.SetParent(RectCurrentItemContainer, false);
        ItemShopController itemShopController = itemOnList.GetComponent<ItemShopController>();
        itemShopController.Build(itemModel);
        CurrentListItem = (itemShopController);
    }
    public void AnimatorShow()
    {
        gameObject.SetActive(true);
    }
    public void AnimatorHide()
    {
        gameObject.SetActive(false);
    }

    public void Show(ItemModel itemModel)
    {
        gameObject.SetActive(true);
        CurrentListItem.UpdateModel(itemModel);
        Animator.SetTrigger("TransitionIn");
    }
}
