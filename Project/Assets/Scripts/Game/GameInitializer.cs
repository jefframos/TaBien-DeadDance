using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameInitializer : MonoBehaviour {
    public Text LabelSoftCurrency;
    public ZombieView Zombie;
    public EnvironmentModel CurrentEnvironmentModel;
    public SpriteRenderer Background;
    public List<GameObject> ToActiveOnInitList;
    void Awake () {
        foreach (var item in ToActiveOnInitList)
        {
            item.SetActive(true);
        }
        GameDataManager.LabelSoftCurrency = LabelSoftCurrency;
        GameDataManager.Zombie = Zombie;
        GameDataManager.Zombie.Build();
        GameDataManager.Init();

        ApplyEnvironmentChanges();
    }
    public void ApplyEnvironmentChanges()
    {
        Background.sprite = CurrentEnvironmentModel.Background;
    }
	
}
