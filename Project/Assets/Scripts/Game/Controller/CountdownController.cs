using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CountdownController : MonoBehaviour {
    public Text label;
    private int _currentValue;
    public RectTransform container;
    public bool started;
    public AudioSource audioSource;
    public AudioClip audioClipAlert;
    // Use this for initialization
    public void Reset (int value = 0) {
        _currentValue = 0;
        label.gameObject.SetActive(true);
        started = true;
        UpdateCounter(value);
    }

    // Update is called once per frame
    public void UpdateCounter (int value) {
        
        label.gameObject.SetActive(true);
        container.transform.localScale = new Vector3(0.5f, 0.5f);
        _currentValue = value;
        label.text = value.ToString();
        container.DOScale(1f, 1f).SetEase(Ease.OutElastic);
        audioSource.PlayOneShot(audioClipAlert);
    }

    public void Finish()
    {
        label.gameObject.SetActive(false);
        started = false;
    }

    internal void Hide()
    {
        label.gameObject.SetActive(false);
    }
}
