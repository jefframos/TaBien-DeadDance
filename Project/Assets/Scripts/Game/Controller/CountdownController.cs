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
    public AudioClip audioClipEnd;
    public Action afterCountdownCallback;
    public int maxCounterValue = 3;
    public float pitchAcum = 0.1f;
    // Use this for initialization
    public void Init (int value, Action afterCountdown) {
        afterCountdownCallback = afterCountdown;
        _currentValue = maxCounterValue;
        label.gameObject.SetActive(true);
        started = true;
        label.text = "";
        Invoke("UpdateCounter", 0.5f);
    }

    // Update is called once per frame
    public void UpdateCounter () {
        
        if(_currentValue == 0)
        {
            LastStep();
            return;
        }
        UpdateLabel(_currentValue.ToString());
        Invoke("UpdateCounter", 1);
        _currentValue--;
    }

    public void LastStep()
    {
        UpdateLabel("GO");
        Invoke("Finish", 1);
        audioSource.PlayOneShot(audioClipEnd);
    }
    public void Finish()
    {
        afterCountdownCallback();
        label.gameObject.SetActive(false);
        started = false;
    }

    internal void UpdateLabel(string value)
    {
        label.gameObject.SetActive(true);
        container.transform.localScale = new Vector3(0.5f, 0.5f);
        label.text = value.ToString();
        container.DOScale(1f, 1f).SetEase(Ease.OutElastic);
        float normalValue = (float)((float)_currentValue / (float)maxCounterValue);
        float pitch = audioSource.pitch + pitchAcum;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClipAlert);
    }
    internal void Hide()
    {
        label.gameObject.SetActive(false);
    }    
}
