using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    public static List<ItemModel> ListItemData;
    // Use this for initialization
    public static void Init()
    {
        ListItemData = new List<ItemModel>();

        ItemModel itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_INIT, ItemBehaviourType.EXTRA_BRAINS);
        itemModel.Name = "Item1";
        itemModel.Level = 1;

        ListItemData.Add(itemModel);

        itemModel = new ItemModel(ItemBehaviourActionType.APPLY_ON_END, ItemBehaviourType.XP_MULTIPLYER);
        itemModel.Name = "Item2";
        itemModel.Level = 1;

        ListItemData.Add(itemModel);

    }
}
