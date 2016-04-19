using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MadnessController : MonoBehaviour {
    public RectTransform MadnessMarkerContainer;
    public Text LabelMadness;
    public GameObject MadnessMarkerPrefab;
    public int MadnessValue = 3;
    public int CurrentMadnessValue;
    private List<MadnessMarkerView> markers;
    // Use this for initialization
    void Start () {
        LabelMadness.text = "MADNESS";
        markers = new List<MadnessMarkerView>();
        for (int i = 0; i < MadnessValue; i++)
        {
            GameObject content = Instantiate(MadnessMarkerPrefab) as GameObject;
            content.transform.SetParent(MadnessMarkerContainer.transform, false);
            markers.Add(content.GetComponent<MadnessMarkerView>());
        }
	}
	
    

    internal void Reset()
    {
        LabelMadness.gameObject.SetActive(false);
        CurrentMadnessValue = 0;
        foreach (var item in markers)
        {
            item.Reset();
        }
    }

    internal void UpdateMadness(int perfectInARow)
    {
        if(perfectInARow == 0)
        {
            Reset();
        }
        else if(perfectInARow <= markers.Count)
        {
            for (int i = 0; i < perfectInARow; i++)
            {
                markers[i].Active();
            }
        }
        CurrentMadnessValue = perfectInARow;
    }

    internal void StartMadness()
    {
        LabelMadness.gameObject.SetActive(true);
    }
}
