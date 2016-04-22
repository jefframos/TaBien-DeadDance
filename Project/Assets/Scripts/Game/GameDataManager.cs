﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour {
    public static int CurrentSoftCurrency { get; internal set; }
    public static Text LabelSoftCurrency;
    public static ZombieView Zombie;
    public static int PurchasePart(PartsModel purchaseModel)
    {
        if(purchaseModel.Value <= CurrentSoftCurrency)
        {
            CurrentSoftCurrency -= purchaseModel.Value;
            PartsDataManager.GetModelById(purchaseModel.PartType ,purchaseModel).Purchased = true;
            return purchaseModel.Value;
        }
        return -1;
    }

    internal static void Init()
    {
        PartsDataManager.Init();

        Zombie.Hide();

        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.HEAD), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.ACESSORY), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.HATS), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.PANTS), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.BODY), true);
        
        ShowZombie();
    }

    public static void ShowZombie()
    {
        Zombie.Show();
    }
    public static void GetCurrency()
    {

    }

    public static void GetInAPPList()
    {

    }

    public static void GetPurchaseItemList()
    {

    }


    public static void UpdatePurchaseLabel()
    {
        if(LabelSoftCurrency == null)
        {
            print("SEM CURRENCY LABEL");
        }
        LabelSoftCurrency.text = CurrentSoftCurrency.ToString();
    }
}
