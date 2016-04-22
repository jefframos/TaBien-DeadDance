using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {
    private ShopSectionType currentType;
    public RectTransform ShopButtonsContainer;
    public GameObject ShopButtonPrefab;
    public List<PartButtonController> CurrentShopButtonsList;
    public List<ShopButtonController> ShopButtonControllerList;
    private bool firstShow;
    public ZombieView Zombie;

    // Use this for initialization
    void Start () {
        firstShow = true;
        currentType = ShopSectionType.NONE;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        print("RESET");
        foreach (var item in ShopButtonControllerList)
        {
            item.Unselect(true);
        }
    }
    public void ActiveShopButton(ShopSectionType type)
    {
        foreach (var item in ShopButtonControllerList)
        {
            if(item.Type == type)
            {
                item.Select(true);
            }
            else
            {
                item.Unselect(true);
            }
        }
    }
    public void ChangeSection(ShopSectionType type)
    {
        if(currentType == type)
        {
            return;
        }
        ActiveShopButton(type);
        switch (type)
        {
            case ShopSectionType.HEAD:
                break;
            case ShopSectionType.BODY:
                break;
            case ShopSectionType.PANTS:
                break;
            case ShopSectionType.HATS:
                break;
            case ShopSectionType.ACESSORY:
                break;
            default:
                break;
        }
        HideSection(type);        
    }

    public void HideSection(ShopSectionType type)
    {
        print(type);
        if(CurrentShopButtonsList != null)
        {
            for (int i = 0; i < CurrentShopButtonsList.Count; i++)
            {
                   CurrentShopButtonsList[i].DestroyPart();
            }
            CurrentShopButtonsList = new List<PartButtonController>();
        }

        currentType = type;

        Invoke("ShowSection", 0.3f);
    }
    public void PartShopCallback(PartsModel partModel, PartButtonController buttonController)
    {
        if (partModel.Active)
        {
            return;
        }
        if (partModel.Purchased)
        {
            switch (partModel.PartType)
            {
                case ShopSectionType.HEAD:
                    break;
                case ShopSectionType.BODY:
                    break;
                case ShopSectionType.PANTS:
                    break;
                case ShopSectionType.HATS:
                    break;
                case ShopSectionType.ACESSORY:
                    break;
                default:
                    break;
            }
            //PartsDataManager.GetModelById(partModel.PartType, partModel);

            List<PartsModel> currentList = PartsDataManager.GetPartListByID(currentType);
            foreach (var item in currentList)
            {
                item.Active = false;
            }

            partModel.Active = true;

            UpdateActiveButtons();

            Zombie.UpdatePart(partModel);
        }
        else
        {
            //try to buy, if return -1 is cause have no money
            int spentCurrency = GameDataManager.PurchasePart(partModel);
            if(spentCurrency >= 0)
            {
                PurchaseCallback(true);
                buttonController.UpdateState(partModel);
            }
            else
            {
                PurchaseCallback(false);
            }
        }
    }
    public void UpdateActiveButtons()
    {
        foreach (var item in CurrentShopButtonsList)
        {
            item.UpdateActiveState();
        }
    }
    public void PurchaseCallback(bool success)
    {
        if (success)
        {
            //HideSection(currentType);
        }
        else
        {
            print("NOMONEY");
        }
    }
    public void ShowSection()
    {
        List<PartsModel> currentList = PartsDataManager.GetPartListByID(currentType);
       
        for (int i = 0; i < currentList.Count; i++)
        {
            currentList[i].PartType = currentType;
            GameObject content = Instantiate(ShopButtonPrefab);
            content.transform.localScale = new Vector3(1f, 1f, 1f);
            content.transform.SetParent(ShopButtonsContainer, false);
            PartButtonController shopButton = content.GetComponent<PartButtonController>();
            shopButton.Build(currentList[i], PartShopCallback);
            shopButton.Show(0.1f * i + (firstShow?0.8f:0f));            
            CurrentShopButtonsList.Add(shopButton);
        }
        firstShow = false;
        UpdateActiveButtons();
    }
}
