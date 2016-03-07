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
    private float timeDecress = 5f;
    public float maxTime = 5f;
    public bool finishing;
    private FeedbackStateView currentState;
    
    
    // Use this for initialization
    void Start () {

        finishing = false;
        currentScale = maxScale;
        updateScale();
        Build(2);

        timeDecress = maxTime;

        button.onClick.AddListener(()=> {
            ClickCallback();
        });

        

        newState.SetActive(true);
        feedbackState.SetActive(false);
    }

    internal void Build(ActionButtonModel model)
    {
        text.text = model.order.ToString();
        
        missState.Hide();
        goodState.Hide();
        greatState.Hide();
        perfectState.Hide();
    }

    private void ClickCallback()
    {
        finish();        
    }

    public void Build(int level)
    {
        
    }
    private void updateScale()
    {
        Vector2 tempScale = outerContent.localScale;
        tempScale.x = currentScale;
        tempScale.y = currentScale;
        outerContent.localScale = tempScale;
    }

    // Update is called once per frame
    void Update () {

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
            timeDecress -= Time.deltaTime;            
            currentScale = (maxScale * (timeDecress / maxTime));
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
        }
        else if (distance < 0.25f)
        {
            currentState = perfectState;
            //print("PERFECT");
        }else if (distance < 0.40f)
        {
            currentState = greatState;
            //print("GREAT");
        }else if (distance < 0.55f)
        {
            currentState = goodState;
            //print("GOOD");
        }
        else if (currentScale < 1)
        {
            currentState = missState;
            currentState.title = "TO LATE";
            //print("TO LATE");
        }
        else
        {
            currentState = missState;
            currentState.title = "TO EARLY";
            //print("TO EARLY");
        }

        currentState.Show();        
    }
}
