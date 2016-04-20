using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {
    private ShopSectionType currentType;
    public RectTransform ShopButtonsContainer;
    public GameObject ShopButtonPrefab;
    public List<PartButtonController> CurrentShopButtonsList;
    private bool firstShow;
    public ZombieView Zombie;
    private List<PartsModel> headData;
    private List<PartsModel> acessoryData;
    private List<PartsModel> hatData;
    private List<PartsModel> pantsData;
    private List<PartsModel> bodyData;
    // Use this for initialization
    void Start () {
        firstShow = true;
        headData = new List<PartsModel>();

        PartsModel tempModel;

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/head";
        headData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/head";
        headData.Add(tempModel);


        acessoryData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "acessory";
        acessoryData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Acessories/Mustache/acessory";
        acessoryData.Add(tempModel);


        hatData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "hat";
        hatData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Hats/Gentleman/hat";
        hatData.Add(tempModel);




        pantsData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/pants";
        pantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/pants";
        pantsData.Add(tempModel);




        bodyData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/body";
        bodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/body";
        bodyData.Add(tempModel);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeSection(ShopSectionType type)
    {
        if(currentType == type)
        {
            return;
        }
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
        if(CurrentShopButtonsList != null)
        {
            for (int i = 0; i < CurrentShopButtonsList.Count; i++)
            {
                   //CurrentShopButtonsList[i].Kill(0.1f * i);
                   CurrentShopButtonsList[i].DestroyPart();
            }
            CurrentShopButtonsList = new List<PartButtonController>();
        }

        currentType = type;

        Invoke("ShowSection", 0.3f);
    }
    public void PartShopCallback(PartsModel partModel)
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
        Zombie.UpdatePart(partModel);
    }
    public void ShowSection()
    {
        List<PartsModel> tempList;
        switch (currentType)
        {
            case ShopSectionType.HEAD:
                tempList = headData;
                break;
            case ShopSectionType.BODY:
                tempList = bodyData;
                break;
            case ShopSectionType.PANTS:
                tempList = pantsData;
                break;
            case ShopSectionType.HATS:
                tempList = hatData;
                break;
            case ShopSectionType.ACESSORY:
                tempList = acessoryData;
                break;
            default:
                tempList = headData;
                break;
        }
        //print(currentType);
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].PartType = currentType;
            GameObject content = Instantiate(ShopButtonPrefab);
            content.transform.localScale = new Vector3(1f, 1f, 1f);
            content.transform.SetParent(ShopButtonsContainer, false);
            PartButtonController shopButton = content.GetComponent<PartButtonController>();
            shopButton.Build(tempList[i], PartShopCallback);
            shopButton.Show(0.1f * i + (firstShow?0.8f:0f));            
            CurrentShopButtonsList.Add(shopButton);
        }
        firstShow = false;
    }
}
