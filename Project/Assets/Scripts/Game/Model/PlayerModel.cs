using UnityEngine;
using System.Collections;
using System;

public class PlayerModel{
    public int Level;
    public float XP;
    public int Life;
    public PlayerInventoryModel EquipablePlayerInventoryModel;
    public int CurrentHeadID;
    public int CurrentHatID;
    public int CurrentAcessoryID;
    public int CurrentBodyID;
    public int CurrentPantsID;
    // Use this for initialization
    public void InitPlayerParts()
    {
        CurrentHeadID = 0;
        CurrentHatID = 0;
        CurrentAcessoryID = 0;
        CurrentBodyID = 0;
        CurrentPantsID = 0;
    }
    public void InitEquipablePlayerInventory () {
        EquipablePlayerInventoryModel = new PlayerInventoryModel();
        EquipablePlayerInventoryModel.Build();

        //ADICIONA UM ITEM
        EquipablePlayerInventoryModel.AddItem(0, InventoryManager.ListItemData[0]);
        EquipablePlayerInventoryModel.AddItem(1, InventoryManager.ListItemData[0]);
    }

    internal void Init()
    {
        Life = 3;
        InitPlayerParts();
        InitEquipablePlayerInventory();
    }
}
