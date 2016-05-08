using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemModel : PurchaseModel
{
    public ItemType Type;
    public int Level;
    public int XP;
    public int ToNextLevel;
    public List<int> LevelsList;
    public List<int> PriceList;
    public List<float> ReferenceValueList;
    public Action<int> ActionLevelUp;
    public int BasePrice;
    public int UnitPrice;
    public string ImageName;
    public string ImagePartName;

    public string Description;

    public ItemBehaviour Behaviour;
    public ItemBehaviourType BehaviourType;
    // Use this for initialization
    public ItemModel(ItemBehaviourActionType itemType, ItemBehaviourType behaviourType)
    {
        BehaviourType = behaviourType;
        switch (BehaviourType)
        {
            case ItemBehaviourType.EXTRA_BRAINS:
                Behaviour = new ExtraBrainBehaviour();
                break;
            case ItemBehaviourType.XP_MULTIPLYER:
                Behaviour = new ItemBehaviour();
                break;
            default:
                Behaviour = new ItemBehaviour();
                break;
        }
        Behaviour.Type = itemType;
        Behaviour.ItemModel = this;
    }
    public void AddXP(int xp)
    {
        XP += xp;
        ToNextLevel = 0;
    }
    public void LevelUp()
    {
        Level++;
        if(ActionLevelUp != null)
        {
            ActionLevelUp(Level);
        }
    }
}
