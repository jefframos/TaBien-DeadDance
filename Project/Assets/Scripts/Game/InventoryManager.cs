using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    public static List<ItemModel> ListEquipableItemData;
    // Use this for initialization
    public static void Init()
    {
        ListEquipableItemData = new List<ItemModel>();

        ItemModel itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_INIT, ItemBehaviourType.EXTRA_BRAINS);
        itemModel.Name = "Item1";
        itemModel.Level = 1;

        ListEquipableItemData.Add(itemModel);

        itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_END, ItemBehaviourType.XP_MULTIPLYER);
        itemModel.Name = "Item2";
        itemModel.Level = 1;

        ListEquipableItemData.Add(itemModel);

        itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_END, ItemBehaviourType.XP_MULTIPLYER);
        itemModel.Name = "Item3";
        itemModel.Level = 1;

        ListEquipableItemData.Add(itemModel);

        itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_END, ItemBehaviourType.XP_MULTIPLYER);
        itemModel.Name = "Item4";
        itemModel.Level = 1;

        ListEquipableItemData.Add(itemModel);

        itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_END, ItemBehaviourType.XP_MULTIPLYER);
        itemModel.Name = "Item5";
        itemModel.Level = 1;

        ListEquipableItemData.Add(itemModel);

    }
}
