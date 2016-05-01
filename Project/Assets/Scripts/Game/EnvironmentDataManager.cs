using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentDataManager : MonoBehaviour
{

    public static List<EnvironmentShopModel> EnvironmentsData;

    public static List<int> ActiveIds;

    public static void Init()
    {
        EnvironmentsData = new List<EnvironmentShopModel>();

        EnvironmentShopModel tempModel;

        tempModel = new EnvironmentShopModel();
        tempModel.Path = "Standard/EnvironmentModel";
        tempModel.ThumbPath = "Standard/thumb";
        tempModel.Purchased = true;
        EnvironmentsData.Add(tempModel);

        tempModel = new EnvironmentShopModel();
        tempModel.Path = "Standard2/EnvironmentModel2";
        tempModel.ThumbPath = "Standard2/thumb";
        tempModel.Purchased = true;
        EnvironmentsData.Add(tempModel);
    }
}
