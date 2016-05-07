using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameDataManager{
    public static int CurrentSoftCurrency { get; internal set; }
    public static EnvironmentModel CurrentEnvironmentModel;

    public static Text LabelSoftCurrency;
    public static ZombieView Zombie;
    public static PlayerModel PlayerModel;
    public static int PurchasePart(PartsModel purchaseModel)
    {
        if(purchaseModel.Value <= CurrentSoftCurrency)
        {
            CurrentSoftCurrency -= purchaseModel.Value;
            PartsDataManager.GetModelById(purchaseModel.PartType ,purchaseModel).Purchased = true;
            UpdateCurrencyLabel();
            return purchaseModel.Value;
        }        
        return -1;
    }

    internal static void Init()
    {
        InventoryManager.Init();
        PartsDataManager.Init();
        EnvironmentDataManager.Init();

        PlayerModel = new PlayerModel();
        PlayerModel.Init();

        PartsDataManager.UpdateInitActiveList();

        Zombie.Hide();

        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.HEAD), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.ACESSORY), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.HATS), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.PANTS), true);
        Zombie.UpdatePart(PartsDataManager.GetActivePart(ShopSectionType.BODY), true);
        
        ShowZombie();

        GameDataManager.CurrentSoftCurrency = 30;

        GameDataManager.CurrentEnvironmentModel = EnvironmentDataManager.EnvironmentsData[0].GetEnvironmentMode();

        UpdateCurrencyLabel();
        
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


    public static void UpdateCurrencyLabel()
    {
        if(LabelSoftCurrency == null)
        {
            Debug.Log("SEM CURRENCY LABEL");
        }
        LabelSoftCurrency.text = CurrentSoftCurrency.ToString();
    }
}
