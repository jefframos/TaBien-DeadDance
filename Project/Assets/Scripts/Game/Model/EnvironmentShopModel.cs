using UnityEngine;
using System.Collections;
using System;

public class EnvironmentShopModel : PurchaseModel
{
    public string Path;
    public string ThumbPath;
    public bool Active;
    public string BasePath = "Game/Environments/";
    private EnvironmentModel environmentModel;
    bool environmentLoaded = false;
    public EnvironmentModel GetEnvironmentMode()
    {
        environmentModel = Resources.Load<EnvironmentModel>(BasePath + Path);
        environmentLoaded = true;
        return environmentModel;

    }
}
