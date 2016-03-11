﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionButtonView : MonoBehaviour {
    public enum FeedbackStateType {MISS, GOOD, GREAT, BAD, PERFECT, TOLATE, TOEARLY };
    [Serializable]
    public class FeedbackStateView
    {
        public Animator animator;
        public Text label;
        public string title;
        public RectTransform content;
        public RectTransform popContent;
        public void Hide()
        {
            content.gameObject.SetActive(false);
        }
        public void Show()
        {
            label.text = title;
            content.gameObject.SetActive(true);
        }
    }
    public FeedbackStateView missState;
    public FeedbackStateView goodState;
    public FeedbackStateView greatState;
    public FeedbackStateView perfectState;
    public FeedbackStateType currentFeedbackState;
    public GameObject newState;
    public GameObject feedbackState;
    public RectTransform innerContent;
    public RectTransform outerContent;
    public Image innerImage;
    public Image outerImage;
    public Text text;
    public Button button;
    public Vector2 scale;
    private float currentScale = 5f;
    public float maxScale = 5f;
    public float minScale = 0.3f;
    //private float timeDecress = 5f;
    private float beatDecress = 5f;
    //public float maxTime = 5f;
    public bool finishing;
    private FeedbackStateView currentState;

    public AudioSource audioSource;
    public AudioClip corretAudioClip;
    public AudioClip perfectAudioClip;
    public AudioClip wrongAudioClip;
    private int maxBeat;

    public Action callback;

    // Use this for initialization
    void Start () {

        finishing = false;

        button.onClick.AddListener(()=> {
            
            ClickCallback();
        });

        

        newState.SetActive(true);
        feedbackState.SetActive(false);
    }

    internal void Build(ActionButtonModel model)
    {
        //maxTime = model.timeToTap;
        maxScale = model.maxScale;

        beatDecress = model.quarterBeatToTap + 4;
        maxBeat = model.quarterBeatToTap;

        currentScale = maxScale;
        //timeDecress = maxTime;
        updateScale();

        text.text = model.order.ToString();

        outerImage.color = model.color;
        innerImage.color = model.color;

        missState.Hide();
        goodState.Hide();
        greatState.Hide();
        perfectState.Hide();
    }

    private void ClickCallback()
    {
        if (finishing)
        {
            return;
        }
        finish();        
    }

    private void updateScale()
    {
        Vector2 tempScale = outerContent.localScale;
        tempScale.x = currentScale;
        tempScale.y = currentScale;
        //if (currentScale <= 1)
        //{
        //    goodState.Show();
        //}
        outerContent.DOScale(tempScale, Time.fixedDeltaTime).SetEase(Ease.Linear);
        //outerContent.localScale = tempScale;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (finishing)
        {
            if (!currentState.animator.GetCurrentAnimatorStateInfo(0).IsName("ActionFeedbackAnimation"))
            {
                currentState.animator.Stop();
                Destroy(this.gameObject);
            }
            return;
        }
        if (currentScale <= minScale)
        {
            finish(true);
        }
        else {
            //timeDecress -= Time.fixedDeltaTime;
            beatDecress -= 1;
            currentScale = (maxScale * (beatDecress / maxBeat));
            updateScale();
        }

    }

    private void finish(bool miss = false)
    {
        finishing = true;

        newState.SetActive(false);
        feedbackState.SetActive(true);
        
        float distance = Vector2.Distance(new Vector2(currentScale, 0), new Vector2(1, 0));
        if (miss)
        {
            currentFeedbackState = FeedbackStateType.MISS;
            currentState = missState;
            //currentState.title = "MISS";
            audioSource.PlayOneShot(wrongAudioClip,0.5f);
        }
        else if (distance < 0.25f)
        {
            currentFeedbackState = FeedbackStateType.PERFECT;
            currentState = perfectState;
            audioSource.PlayOneShot(perfectAudioClip, 0.5f);
            //print("PERFECT");
        }
        else if (distance < 0.40f)
        {
            currentFeedbackState = FeedbackStateType.GREAT;
            currentState = greatState;
            audioSource.PlayOneShot(corretAudioClip, 0.5f);
            //print("GREAT");
        }
        else if (distance < 0.55f)
        {
            currentFeedbackState = FeedbackStateType.GOOD;
            currentState = goodState;
            audioSource.PlayOneShot(corretAudioClip, 0.5f);
            //print("GOOD");
        }
        else if (currentScale < 1)
        {
            currentFeedbackState = FeedbackStateType.TOLATE;
            currentState = missState;
            //currentState.title = "TO LATE";
            audioSource.PlayOneShot(wrongAudioClip, 0.5f);
            //print("TO LATE");
        }
        else
        {
            currentFeedbackState = FeedbackStateType.TOEARLY;
            currentState = missState;
            //currentState.title = "TO EARLY";
            audioSource.PlayOneShot(wrongAudioClip, 0.5f);
            //print("TO EARLY");
        }
        callback();
        currentState.Show();        
    }
}
