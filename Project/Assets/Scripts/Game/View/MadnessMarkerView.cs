using UnityEngine;
using System.Collections;
using System;

public class MadnessMarkerView : MonoBehaviour {
    public RectTransform RectActive;
    public RectTransform RectUnactive;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Reset()
    {
        RectActive.gameObject.SetActive(false);
        RectUnactive.gameObject.SetActive(true);
    }
    internal void Active()
    {
        RectActive.gameObject.SetActive(true);
        RectUnactive.gameObject.SetActive(false);
    }
}
