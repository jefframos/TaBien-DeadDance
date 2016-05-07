using UnityEngine;
using System.Collections;

public class ExtraBrainBehaviour : ItemBehaviour {
    public int ExtraBrain = 1;
    public override void ApplyEffects(InGameModel gameModel)
    {
        gameModel.ExtraBrain += ExtraBrain * ItemModel.Level;
    }
}
