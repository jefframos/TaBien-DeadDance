using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class ActionParticleView : MonoBehaviour {
    public Color color;
    public int quant = 3;
    public Vector3 destiny;
    public Vector3 initPos;
    public float delay;
    public float time = 2f;
    public RectTransform imageContainer;
    public Image image;
    public Image brainImage;
    internal RectTransform rectTarget;
    internal Action endTweenCallback;
    public AudioSource audioSource;
    public List<AudioClip> audioClipCoin;
    private bool _finished;
    // Update is called once per frame
    void Update () {
	    if(_finished && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}

    internal void SetSpecial()
    {
        brainImage.gameObject.SetActive(true);
        image.gameObject.SetActive(false);

    }
    internal void Build(bool isSpecial, int points, int order)
    {
        _finished = false;
        //if (time > 0.8f)
        //{
        //    time = 0.8f;
        //}

        time = 0.8f;
        if (isSpecial)
        {
            SetSpecial();
        }
        else {
            brainImage.gameObject.SetActive(false);
            image.gameObject.SetActive(true);
            image.color = color;
        }
        
        this.gameObject.SetActive(true);
        
        imageContainer.localPosition = initPos;
        //imageContainer.DOMove(destiny, time).SetDelay(delay).OnComplete(() => {
        imageContainer.DOMove(destiny, time).SetDelay(order * 0.1f).OnComplete(() => {
            Vector2 scl = new Vector2(1, 1);
            rectTarget.localScale = scl;
            rectTarget.DOScale(new Vector2(1.5f, 1.5f), 0.5f).From();
            if (endTweenCallback != null)
            {                
                endTweenCallback();
            }
            int audioID = 1;// UnityEngine.Random.Range(0, audioClipCoin.Count);
            audioSource.pitch = 1 + order * UnityEngine.Random.Range(0.1f, 0.2f);//UnityEngine.Random.Range(1f, 1.2f);
            audioSource.PlayOneShot(audioClipCoin[audioID]);
            _finished = true;
        });
        imageContainer.DOScale(new Vector3(0.5f, 0.5f, 1f), time).SetDelay(delay);        
    }
}
