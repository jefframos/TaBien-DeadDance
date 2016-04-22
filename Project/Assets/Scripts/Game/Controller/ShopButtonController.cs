using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShopButtonController : MonoBehaviour {

    public Button Button;
    public ShopSectionType Type;
    public ShopController ShopController;
    public RectTransform SelectedState;
    public RectTransform UnselectedState;
    // Use this for initialization
    void Start () {
        Button.onClick.AddListener(()=>{
            ShopController.ChangeSection(Type);
        });
    }

    public void Select(bool force = false)
    {
        SelectedState.gameObject.SetActive(true);
        UnselectedState.gameObject.SetActive(false);
    }
    public void Unselect(bool force = false)
    {
        SelectedState.gameObject.SetActive(false);
        UnselectedState.gameObject.SetActive(true);
    }
}
