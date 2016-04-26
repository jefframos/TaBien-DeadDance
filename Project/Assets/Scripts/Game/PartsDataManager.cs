using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PartsDataManager : MonoBehaviour {

    public static List<PartsModel> HeadData;
    public static List<PartsModel> AcessoryData;
    public static List<PartsModel> HatData;
    public static List<PartsModel> PantsData;
    public static List<PartsModel> BodyData;

    public static List<int> ActiveIds;


    public static void Init()
    {
        
        HeadData = new List<PartsModel>();

        PartsModel tempModel;

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/head";
        tempModel.ThumbPath = "Hats/Gentleman/hat";
        tempModel.Purchased = true;
        HeadData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/head";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = true;
        HeadData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie3/head";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        HeadData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie4/head";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        HeadData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie5/head";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        HeadData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie6/head";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        HeadData.Add(tempModel);


        AcessoryData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "acessory";
        tempModel.ThumbPath = "Hats/Gentleman/hat";
        tempModel.Purchased = true;
        AcessoryData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Acessories/Mustache/acessory";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = true;
        AcessoryData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Acessories/Mustache/acessory";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        AcessoryData.Add(tempModel);


        HatData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "hat";
        tempModel.ThumbPath = "Hats/Gentleman/hat";
        tempModel.Purchased = true;
        HatData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Hats/Gentleman/hat";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = true;
        HatData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Hats/Gentleman/hat";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        HatData.Add(tempModel);



        PantsData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/pants";
        tempModel.ThumbPath = "Hats/Gentleman/hat";
        tempModel.Purchased = true;
        PantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/pants";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = true;
        PantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie3/pants";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        PantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie4/pants";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        PantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie5/pants";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        PantsData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie6/pants";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        PantsData.Add(tempModel);




        BodyData = new List<PartsModel>();

        tempModel = new PartsModel();
        tempModel.Path = "Zombie1/body";
        tempModel.ThumbPath = "Hats/Gentleman/hat";
        tempModel.Purchased = true;
        BodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie2/body";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = true;
        BodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie3/body";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        BodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie4/body";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        BodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie5/body";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        BodyData.Add(tempModel);

        tempModel = new PartsModel();
        tempModel.Path = "Zombie6/body";
        tempModel.ThumbPath = "Acessories/Mustache/acessory";
        tempModel.Purchased = false;
        BodyData.Add(tempModel);


        for (int i = 0; i < HeadData.Count; i++)
        {
            HeadData[i].Id = i;
            HeadData[i].PartType = ShopSectionType.HEAD;
            HeadData[i].Name = "Head"+i;
        }
        for (int i = 0; i < AcessoryData.Count; i++)
        {
            AcessoryData[i].Id = i;
            AcessoryData[i].PartType = ShopSectionType.ACESSORY;
            AcessoryData[i].Name = "Acessory" + i;
        }
        for (int i = 0; i < HatData.Count; i++)
        {
            HatData[i].Id = i;
            HatData[i].PartType = ShopSectionType.HATS;
            HatData[i].Name = "Hat" + i;
        }
        for (int i = 0; i < BodyData.Count; i++)
        {
            BodyData[i].Id = i;
            BodyData[i].PartType = ShopSectionType.BODY;
            BodyData[i].Name = "Body" + i;
        }
        for (int i = 0; i < PantsData.Count; i++)
        {
            PantsData[i].Id = i;
            PantsData[i].PartType = ShopSectionType.PANTS;
            PantsData[i].Name = "Pant" + i;
        }


        ActiveIds = new List<int>();
        //ActiveIds.Add(UnityEngine.Random.Range(0, HeadData.Count));//HEAD
        //ActiveIds.Add(UnityEngine.Random.Range(0, AcessoryData.Count));//ACESSORY
        //ActiveIds.Add(UnityEngine.Random.Range(0, HatData.Count));//HAT
        //ActiveIds.Add(UnityEngine.Random.Range(0, BodyData.Count));//BODY
        //ActiveIds.Add(UnityEngine.Random.Range(0, PantsData.Count));//PANTS

        ActiveIds.Add(0);//HEAD
        ActiveIds.Add(0);//ACESSORY
        ActiveIds.Add(0);//HAT
        ActiveIds.Add(0);//BODY
        ActiveIds.Add(0);//PANTS

        PurchaseEverything();
        UpdateInitActiveList();

    }
    //purchase everything
    internal static void PurchaseEverything()
    {
        for (int i = 1; i < HeadData.Count; i++)
        {
            HeadData[i].Purchased = false;
            HeadData[i].Value = i * 2;
        }
        for (int i = 1; i < AcessoryData.Count; i++)
        {
            AcessoryData[i].Purchased = false;
            AcessoryData[i].Value = i * 2;
        }
        for (int i = 1; i < HatData.Count; i++)
        {
            HatData[i].Purchased = false;
            HatData[i].Value = i * 2;
        }
        for (int i = 1; i < BodyData.Count; i++)
        {
            BodyData[i].Purchased = false;
            BodyData[i].Value = i * 2;
        }
        for (int i = 1; i < PantsData.Count; i++)
        {
            PantsData[i].Purchased = false;
            PantsData[i].Value = i * 2;
        }
    }
    //initial actives
    internal static void UpdateInitActiveList()
    {
        for (int i = 0; i < HeadData.Count; i++)
        {
            if(HeadData[i].Id == ActiveIds[0])
            {
                HeadData[i].Active = true;
            }
            else
            {
                HeadData[i].Active = false;
            }           
        }
        for (int i = 0; i < AcessoryData.Count; i++)
        {
            if (AcessoryData[i].Id == ActiveIds[1])
            {
                AcessoryData[i].Active = true;
            }
            else
            {
                AcessoryData[i].Active = false;
            }
        }
        for (int i = 0; i < HatData.Count; i++)
        {
            if (HatData[i].Id == ActiveIds[2])
            {
                HatData[i].Active = true;
            }
            else
            {
                HatData[i].Active = false;
            }
        }
        for (int i = 0; i < BodyData.Count; i++)
        {
            if (BodyData[i].Id == ActiveIds[3])
            {
                BodyData[i].Active = true;
            }
            else
            {
                BodyData[i].Active = false;
            }
        }
        for (int i = 0; i < PantsData.Count; i++)
        {
            if (PantsData[i].Id == ActiveIds[4])
            {
                PantsData[i].Active = true;
            }
            else
            {
                PantsData[i].Active = false;
            }
        }
    }
    //get active part
    internal static PartsModel GetActivePart(ShopSectionType type)
    {
        PartsModel tempPartModel;
        switch (type)
        {
            case ShopSectionType.HEAD:
                tempPartModel = HeadData.Find(myItem => true == myItem.Active);
                break;
            case ShopSectionType.BODY:
                tempPartModel = BodyData.Find(myItem => true == myItem.Active);
                break;
            case ShopSectionType.PANTS:
                tempPartModel = PantsData.Find(myItem => true == myItem.Active);
                break;
            case ShopSectionType.HATS:
                tempPartModel = HatData.Find(myItem => true == myItem.Active);
                break;
            case ShopSectionType.ACESSORY:
                tempPartModel = AcessoryData.Find(myItem => true == myItem.Active);
                break;
            default:
                print("GetActivePart Default");
                tempPartModel = HeadData.Find(myItem => true == myItem.Active);
                break;
        }

        return tempPartModel;
    }
    //return part modal list
    internal static List<PartsModel> GetPartListByID(ShopSectionType type)
    {
        List<PartsModel> currentList;
        switch (type)
        {
            case ShopSectionType.HEAD:
                currentList = PartsDataManager.HeadData;
                break;
            case ShopSectionType.BODY:
                currentList = PartsDataManager.BodyData;
                break;
            case ShopSectionType.PANTS:
                currentList = PartsDataManager.PantsData;
                break;
            case ShopSectionType.HATS:
                currentList = PartsDataManager.HatData;
                break;
            case ShopSectionType.ACESSORY:
                currentList = PartsDataManager.AcessoryData;
                break;
            default:
                print("GetPartListByID Default");
                currentList = PartsDataManager.HatData;
                break;
        }
        return currentList;
    }
    //return modal
    internal static PurchaseModel GetModelById(ShopSectionType type, PurchaseModel partsModel)
    {
        PartsModel tempPartModel;
        switch (type)
        {
            case ShopSectionType.HEAD:
                tempPartModel = HeadData.Find(myItem => partsModel.Id == myItem.Id);
                break;
            case ShopSectionType.BODY:
                tempPartModel = BodyData.Find(myItem => partsModel.Id == myItem.Id);
                break;
            case ShopSectionType.PANTS:
                tempPartModel = PantsData.Find(myItem => partsModel.Id == myItem.Id);
                break;
            case ShopSectionType.HATS:
                tempPartModel = HatData.Find(myItem => partsModel.Id == myItem.Id);
                break;
            case ShopSectionType.ACESSORY:
                tempPartModel = AcessoryData.Find(myItem => partsModel.Id == myItem.Id);
                break;
            default:
                print("GetModelById Default");
                tempPartModel = HeadData.Find(myItem => partsModel.Id == myItem.Id);
                break;
        }

        return tempPartModel;
    }
    //set active part model
    internal static void SetActivePartModel(PartsModel partsModel)
    {
        switch (partsModel.PartType)
        {
            case ShopSectionType.HEAD:
                ActiveIds[0] = partsModel.Id;
                break;
            case ShopSectionType.BODY:
                ActiveIds[3] = partsModel.Id;
                break;
            case ShopSectionType.PANTS:
                ActiveIds[4] = partsModel.Id;
                break;
            case ShopSectionType.HATS:
                ActiveIds[2] = partsModel.Id;
                break;
            case ShopSectionType.ACESSORY:
                ActiveIds[1] = partsModel.Id;
                break;
            default:
                print("SetActivePartModel Default");
                break;
        }
    }
}
