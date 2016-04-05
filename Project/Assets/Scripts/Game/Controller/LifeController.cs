using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour {
    public List<LifeHearthView> hearthList;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    public void Reset()
    {
        for (int i = 0; i < hearthList.Count; i++)
        {
            hearthList[i].Reset();
        }
    }
	public void UpdateHearthList (int life)
    {
        for (int i = 0; i < hearthList.Count; i++)
        {
            if (i >= life)
            {
                hearthList[i].Unfill();
            }
            else {
                hearthList[i].Fill();
            }
        }
    }
}
