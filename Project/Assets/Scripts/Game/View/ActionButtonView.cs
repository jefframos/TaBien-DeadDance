using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class ActionButtonView : MonoBehaviour {
    public GameObject newState;
    public GameObject feedbackState;
    public RectTransform innerContent;
    public RectTransform outerContent;
    public Text text;
    public Button button;
    public Vector2 scale;
    private float currentScale = 5f;
    public float maxScale = 5f;
    public float minScale = 0.5f;
    private float timeDecress = 5f;
    public float maxTime = 5f;
    public bool finishing;
    public Animator feedbackAnimator;
    // Use this for initialization
    void Start () {

        finishing = false;
        updateScale();
        Build(2);

        timeDecress = maxTime;

        button.onClick.AddListener(()=> {
            ClickCallback();
        });

        newState.SetActive(true);
        feedbackState.SetActive(false);
    }

    private void ClickCallback()
    {
        print("CLICK");
        finish();        
    }

    public void Build(int level)
    {
        text.text = level.ToString();
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
            if (!feedbackAnimator.GetCurrentAnimatorStateInfo(0).IsName("ActionFeedbackAnimation"))
            {
                feedbackAnimator.Stop();
                Destroy(this.gameObject);
                // Do something.
            }
            return;
        }
        if (currentScale <= minScale)
        {
            //updateScale();
            finish();
        }
        else {
            timeDecress -= Time.deltaTime;
            
            currentScale = (maxScale * (timeDecress / maxTime));
            updateScale();
            //print(maxScale +" - "+ maxTime + " - " + timeDecress + " - " + currentScale);
        }

    }

    private void finish()
    {
        finishing = true;

        newState.SetActive(false);
        feedbackState.SetActive(true);
        //StartCoroutine(feedbackState.animation.PlayWithOptions(
        //         animToPlay,
        //         () =>
        //         {
        //             Debug.Log("ANIMATION FINISHED, LOADING SCENE X");
        //             Application.LoadLevel("SceneX");
        //         }
        //         )
        //     );

        
        //print(currentScale);
    }
}
