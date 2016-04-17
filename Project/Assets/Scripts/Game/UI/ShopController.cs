using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {
    private ShopSectionType currentType;
    public RectTransform ShopButtonsContainer;
    public GameObject ShopButtonPrefab;
    public List<GameObject> CurrentShopButtonsList;
    // Use this for initialization
    void Start () {
	
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
            foreach (GameObject item in CurrentShopButtonsList)
            {
                Destroy(item);
            }
            CurrentShopButtonsList = new List<GameObject>();
        }

        currentType = type;

        Invoke("ShowSection", 0.5f);
    }
    public void ShowSection()
    {
        //print(currentType);
        for (int i = 0; i < 5; i++)
        {
            GameObject content = Instantiate(ShopButtonPrefab);
            content.transform.localScale = new Vector3(1f, 1f, 1f);
            content.transform.SetParent(ShopButtonsContainer, false);

            CurrentShopButtonsList.Add(content);
        }

    }
}
