using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemShopController : MonoBehaviour
{
    public Image Image;
    public Text Title;
    public Text Description;
    public Text Level1;
    public Text Level2;
    public Text Price;
    public LevelGaugeView LevelGaugeView;
    public Button ButtonPurchase;
    public Button ButtonEquip;
    public Button ButtonRemove;
    public ItemModel ItemModel;
    public GameObject ButtonsContainer;
    public Action<ItemShopController> PurchaseCallback;
    public Action<ItemShopController> EquipCallback;
    public Action<ItemShopController> RemoveCallback;
    // Use this for initialization
    public void Build(ItemModel itemModel)
    {
        ItemModel = itemModel;

        Title.text = ItemModel.Name;
        Description.text = ItemModel.Description;
        Level1.text = ItemModel.Level.ToString();
        Level2.text = (ItemModel.Level + 1).ToString(); //maybe is MAX here
        Price.text = ItemModel.BasePrice.ToString();
        LevelGaugeView.UpdateBar(0.5f);
    }
    internal void UpdateModel(ItemModel itemModel)
    {
        Build(itemModel);
    }
}
