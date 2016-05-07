using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerInventoryModel{
    public List<ItemSlotModel> ListItemSlotModel;

    internal void Build()
    {
        ListItemSlotModel = new List<ItemSlotModel>();

        ListItemSlotModel.Add(new ItemSlotModel());
        ListItemSlotModel.Add(new ItemSlotModel());
        ListItemSlotModel.Add(new ItemSlotModel());
    }
    public void AddItem(int id, ItemModel item)
    {
        if(id < ListItemSlotModel.Count)
        {
            ListItemSlotModel[id].ItemModel = item;
        }
    }
}
