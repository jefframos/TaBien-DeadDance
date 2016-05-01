using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour {
    public Text LabelSoftCurrency;
    public ZombieView Zombie;
    public EnvironmentModel CurrentEnvironmentModel;
    public SpriteRenderer Background;
    void Awake () {
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
