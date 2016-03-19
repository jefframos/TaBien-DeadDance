using UnityEngine;
using System.Collections;

public class LevelGaugeView : MonoBehaviour {
    public RectTransform levelBar;
    public RectTransform areaBar;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateBar (float value) {
        Vector2 tempSizeDelta = levelBar.sizeDelta;
        tempSizeDelta.x = value * areaBar.rect.width - areaBar.rect.width;
        levelBar.sizeDelta = tempSizeDelta;
    }
}
