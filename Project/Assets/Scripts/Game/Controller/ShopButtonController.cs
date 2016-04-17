using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour {

    public Button Button;
    public ShopSectionType Type;
    public ShopController ShopController;
    // Use this for initialization
    void Start () {
        Button.onClick.AddListener(()=>{
            ShopController.ChangeSection(Type);
        });
    }
}
