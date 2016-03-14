using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class ActionButtonView : MonoBehaviour {
    
    [Serializable]
    public class FeedbackStateView
    {
        public Animator animator;
        public Text label;
        public Text labelBack;
        public string title;
        public RectTransform content;
        public RectTransform popContent;
        public CanvasGroup canvasGroup;
        public Color color;
        public Image inner;
        public void Hide()
        {
            content.gameObject.SetActive(false);
        }
        public void Show()
        {
            inner.color = color;
            if (canvasGroup != null)
            {
                //canvasGroup.DOFade(1f, 0.5f);
            }
            label.text = title;
            labelBack.text = title;
            content.gameObject.SetActive(true);
        }
    }
    public CanvasGroup canvasGroup;
    public FeedbackStateView missState;
    public FeedbackStateView goodState;
    public FeedbackStateView greatState;
    public FeedbackStateView perfectState;

    public FeedbackStateType currentFeedbackState;
    public GameObject newState;
    //public GameObject feedbackState;

    [Serializable]
    public class ArrowsView
    {
        public GameObject container;
        public GameObject top;
        public GameObject right;
        public GameObject bottom;
        public GameObject left;

        internal void Hide()
        {
            container.SetActive(false);
            top.SetActive(false);
            right.SetActive(false);
            bottom.SetActive(false);
            left.SetActive(false);
        }

        internal void Show(BehaviourType behaviour)
        {
            container.SetActive(true);
            //active the inverse arrow
            switch (behaviour)
            {
                case BehaviourType.FROM_TOP:
                    bottom.SetActive(true);
                    break;
                case BehaviourType.FROM_RIGHT:
                    left.SetActive(true);
                    break;
                case BehaviourType.FROM_BOTTOM:
                    top.SetActive(true);
                    break;
                case BehaviourType.FROM_LEFT:
                    right.SetActive(true);
                    break;
            }
        }
    }

    public ArrowsView arrows;
    public RectTransform innerContent;
    public RectTransform outerContent;
    public Image innerImage;
    public Image outerImage;
    public Text text;
    public Text textBack;
    public Button button;
    public Vector2 scale;
    private float currentBehaviourFactor = 5f;

    public float maxBehaviourFactor = 5f;
    public float minBehaviourFactor = 0.3f;

    //private float timeDecress = 5f;
    private float beatDecress = 5f;
    //public float maxTime = 5f;
    public bool finishing;
    public FeedbackStateView currentState;

    public AudioSource audioSource;
    public AudioClip corretAudioClip;
    public AudioClip perfectAudioClip;
    public AudioClip wrongAudioClip;
    private int maxBeat;
    private BehaviourType behaviour;

    public Action finishCallback;
    private bool builded;

    // Use this for initialization
    void Start () {

        finishing = false;

        button.onClick.AddListener(()=> {
            
            ClickCallback();
        });

        

        newState.SetActive(true);
        
    }

    internal void Build(ActionButtonModel model)
    {
        builded = true;
        //maxTime = model.timeToTap;
        maxBehaviourFactor = model.maxScale;

        beatDecress = model.quarterBeatToTap + 4;
        maxBeat = model.quarterBeatToTap;

        currentBehaviourFactor = maxBehaviourFactor;
        currentBehaviourFactor = (maxBehaviourFactor * (beatDecress / maxBeat));
        //timeDecress = maxTime;
        arrows.Hide();



        if (model.order > 0)
        {
            text.text = model.order.ToString();
            textBack.text = model.order.ToString();
            textBack.color = model.color;
        }
        else
        {
            text.text = "";
            textBack.text = "";
        }

        outerImage.color = model.color;
        innerImage.color = model.color;

        behaviour = model.behaviour;

        missState.Hide();
        goodState.Hide();
        greatState.Hide();
        perfectState.Hide();

        //if (canvasGroup != null)
        //{
        //    canvasGroup.DOFade(1f, 0.5f);
        //}

        updateBehaviour(true);

    }

    private void ClickCallback()
    {
        if (finishing)
        {
            return;
        }
        finish();        
    }

    private void updateBehaviour(bool first = false)
    {
        //if (currentBehaviourFactor <= 1)
        //{
        //    goodState.Show();
        //}
        if(behaviour == BehaviourType.SCALE)
        {
            Vector2 tempScale = outerContent.localScale;
            tempScale.x = currentBehaviourFactor;
            tempScale.y = currentBehaviourFactor;
            outerContent.DOScale(tempScale, first ? 0 : Time.fixedDeltaTime).SetEase(Ease.Linear);
        }
        else
        {
            
            Vector2 position = outerContent.localPosition;
            switch (behaviour)
            {
                case BehaviourType.FROM_TOP:
                    position.y = (currentBehaviourFactor - 1) * outerContent.rect.height;                    
                    break;
                case BehaviourType.FROM_RIGHT:                    
                    position.x = (currentBehaviourFactor - 1) * outerContent.rect.width;
                    break;
                case BehaviourType.FROM_BOTTOM:
                    position.y = -(currentBehaviourFactor - 1) * outerContent.rect.height;
                    break;
                case BehaviourType.FROM_LEFT:
                    position.x = -(currentBehaviourFactor - 1) * outerContent.rect.width;
                    break;
                default:
                    break;
            }
            arrows.Show(behaviour);
            outerContent.DOKill(true);
            outerContent.DOLocalMove(position, first ? 0 : Time.fixedDeltaTime).SetEase(Ease.Linear);
        }        
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!builded)
        {
            return;
        }
        if (finishing)
        {
            if (!currentState.animator.GetCurrentAnimatorStateInfo(0).IsName("ActionFeedbackAnimation"))
            {
                currentState.animator.Stop();
                Destroy(this.gameObject);
            }
            return;
        }
        if (currentBehaviourFactor <= minBehaviourFactor)
        {
            finish(true);
        }
        else {
            //timeDecress -= Time.fixedDeltaTime;
            beatDecress -= 1;
            currentBehaviourFactor = (maxBehaviourFactor * (beatDecress / maxBeat));
            updateBehaviour();
        }

    }

    private void finish(bool miss = false)
    {
        finishing = true;
        newState.SetActive(false);        
        float distance = Vector2.Distance(new Vector2(currentBehaviourFactor, 0), new Vector2(1, 0));
        if (miss)
        {
            currentFeedbackState = FeedbackStateType.MISS;
            currentState = missState;
            //currentState.title = "MISS";
            audioSource.PlayOneShot(wrongAudioClip,0.5f);
        }
        else if (distance < 0.20f)
        {
            currentFeedbackState = FeedbackStateType.PERFECT;
            currentState = perfectState;
            audioSource.PlayOneShot(perfectAudioClip, 0.5f);
        }
        else if (distance < 0.30f)
        {
            currentFeedbackState = FeedbackStateType.GREAT;
            currentState = greatState;
            audioSource.PlayOneShot(corretAudioClip, 0.5f);
        }
        else if (distance < 0.50f)
        {
            currentFeedbackState = FeedbackStateType.GOOD;
            currentState = goodState;
            audioSource.PlayOneShot(corretAudioClip, 0.5f);
        }
        else if (currentBehaviourFactor < 1)
        {
            currentFeedbackState = FeedbackStateType.TOLATE;
            currentState = missState;
            //currentState.title = "TO LATE";
            audioSource.PlayOneShot(wrongAudioClip, 0.5f);
        }
        else
        {
            currentFeedbackState = FeedbackStateType.TOEARLY;
            currentState = missState;
            //currentState.title = "TO EARLY";
            audioSource.PlayOneShot(wrongAudioClip, 0.5f);
        }
        finishCallback();
        currentState.Show();        
    }
}
