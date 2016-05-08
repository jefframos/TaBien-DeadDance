using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EquipableItensController : MonoBehaviour {
    public GameObject PrefabItemSlot;
    public RectTransform RectItemSlotContainer;
    public List<ItemSlotController> ListItemSlot;
    public EquipableItemPopUpController EquipableItemPopUp;

    // Use this for initialization
    public void Build()
    {
        for (int i = 0; i < GameDataManager.PlayerModel.EquipablePlayerInventoryModel.ListItemSlotModel.Count; i++)
        {
            addSlot(GameDataManager.PlayerModel.EquipablePlayerInventoryModel.ListItemSlotModel[i]);
        }
        
    }

    private void addSlot(ItemSlotModel itemSlotModel)
    {
        GameObject itemSlot = Instantiate(PrefabItemSlot);
        itemSlot.transform.SetParent(RectItemSlotContainer, false);
        ItemSlotController itemSlotController = itemSlot.GetComponent<ItemSlotController>();
        itemSlotController.Build(itemSlotModel, SlotClickCallback);
        ListItemSlot.Add(itemSlotController);
    }

    private void SlotClickCallback(ItemSlotModel itemSlotModel)
    {
        EquipableItemPopUp.Show(itemSlotModel.ItemModel);
        print(itemSlotModel.ItemModel.Name);
    }
}
