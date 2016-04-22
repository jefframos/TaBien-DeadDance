using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour {
    public Text LabelSoftCurrency;
    public ZombieView Zombie;

    void Awake () {
        GameDataManager.LabelSoftCurrency = LabelSoftCurrency;
        GameDataManager.Zombie = Zombie;
        GameDataManager.Zombie.Build();

        GameDataManager.Init();
    }
	
}
