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
    public TrailRenderer Trail;
    public GameObject QuantContainer;
    public Text QuantLabel1;
    public Text QuantLabel2;
    // Update is called once per frame
    void Update () {
	    if(_finished && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}

    internal void SetSpecial(int quant)
    {
        brainImage.gameObject.SetActive(true);
        image.gameObject.SetActive(false);

        if(quant > 1)
        {

            QuantContainer.SetActive(true);

            QuantLabel1.text = "X" + quant;
            QuantLabel2.text = "X" + quant;

            float rndAngle = UnityEngine.Random.Range(-20, 20);
            Quaternion angle = Quaternion.Euler(0, 0, rndAngle);
            QuantContainer.transform.localRotation = angle;
        }

    }
    internal void Build(bool isSpecial, int points, int order)
    {
        QuantContainer.SetActive(false);
        _finished = false;
        //if (time > 0.8f)
        //{
        //    time = 0.8f;
        //}

        time = 0.8f;
        if (isSpecial)
        {
            SetSpecial(order);
        }
        else {
            brainImage.gameObject.SetActive(false);
            image.gameObject.SetActive(true);
            image.color = color;
            //Trail.material.SetColor("_TintColor", color);
        }
        
        this.gameObject.SetActive(true);
        
        
        if (isSpecial)
        {
            initPos.x = 0;
            imageContainer.localPosition = initPos;
            Sequence seq = DOTween.Sequence();
            imageContainer.localScale = new Vector3();
            imageContainer.DOScale(0.5f, 0.5f).SetDelay(1.7f);
            float size = 1f + order * 0.1f;
            if(size > 1.6f)
            {
                size = 1.6f;
            }
            seq.Append(imageContainer.DOScale(size, 1.2f).SetEase(Ease.OutElastic).SetDelay(0.5f));
            //seq.Insert(1, imageContainer.DOScale(0.5f, 0.5f));
            seq.Append(imageContainer.DOMove(destiny, 0.3f));
            seq.OnComplete(() =>
            {
                imageContainer.DOScale(new Vector2(0f, 0f), 0.3f);
                if (endTweenCallback != null)
                {
                    endTweenCallback();
                }
                int audioID = 1;// UnityEngine.Random.Range(0, audioClipCoin.Count);
                audioSource.pitch = 1 + order * UnityEngine.Random.Range(0.1f, 0.2f);//UnityEngine.Random.Range(1f, 1.2f);
                audioSource.PlayOneShot(audioClipCoin[audioID]);
                _finished = true;
            });


        }
        else {
            imageContainer.localPosition = initPos;
            imageContainer.DOMove(destiny, time).SetDelay(order * 0.1f).OnComplete(() =>
            {
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
}
