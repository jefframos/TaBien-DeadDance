using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionButtonView : MonoBehaviour {

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

    public GameObject newState;
    public GameObject feedbackState;
    public RectTransform innerContent;
    public RectTransform outerContent;
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

        beatDecress = model.quarterBeatToTap;
        maxBeat = model.quarterBeatToTap;

        currentScale = maxScale;
        //timeDecress = maxTime;
        updateScale();

        text.text = model.order.ToString();
        
        missState.Hide();
        goodState.Hide();
        greatState.Hide();
        perfectState.Hide();
    }

    private void ClickCallback()
    {
        print("CLICK");
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
        if(currentScale <= 1)
        {
            goodState.Show();
        }
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
            currentState = missState;
            currentState.title = "MISS";
            audioSource.PlayOneShot(wrongAudioClip);
        }
        else if (distance < 0.25f)
        {
            currentState = perfectState;
            audioSource.PlayOneShot(perfectAudioClip);
            //print("PERFECT");
        }
        else if (distance < 0.40f)
        {
            currentState = greatState;
            audioSource.PlayOneShot(corretAudioClip);
            //print("GREAT");
        }
        else if (distance < 0.55f)
        {
            currentState = goodState;
            audioSource.PlayOneShot(corretAudioClip);
            //print("GOOD");
        }
        else if (currentScale < 1)
        {
            currentState = missState;
            currentState.title = "TO LATE";
            audioSource.PlayOneShot(wrongAudioClip);
            //print("TO LATE");
        }
        else
        {
            currentState = missState;
            currentState.title = "TO EARLY";
            audioSource.PlayOneShot(wrongAudioClip);
            //print("TO EARLY");
        }

        currentState.Show();        
    }
}
