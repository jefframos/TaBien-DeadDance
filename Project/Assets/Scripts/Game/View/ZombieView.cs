using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class ZombieView : MonoBehaviour {

    public RectTransform leftArm;
    public RectTransform rightArm;
    public RectTransform leftLeg;
    public RectTransform rightLeg;

    public Animator headAnimator;
    public Animator bodyAnimator;

    public Vector3 standardLeftArm;

    private bool started;
    // Use this for initialization
    void Awake()
    {
        standardLeftArm = leftArm.localPosition;
        started = false;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Beat()
    {
        if (!started)
        {
            started = true;
            bodyAnimator.Play("BodyStandard", -1, 0f);
        }
        headAnimator.Play("BeatStandard", -1, 0.1f);

        //headAnimator.CrossFade("BeatStandard", 0);
        //bodyAnimator.Play("BodyStandard", -1, 0f);

    }

    internal void updatePart(Vector3 tempV3)
    {
        //leftArm.DOKill(); 
        //leftArm.DOLocalMove(tempV3, 0.2f);
        //leftArm.DOLocalMove(standardLeftArm, 0.2f).SetDelay(0.25f);
        //print(tempV3);
    }

    internal void updateLevel()
    {
        bodyAnimator.CrossFade("Standard1", 0.5f);
    }

    internal void Reset()
    {
        bodyAnimator.Play("BodyStandard", -1, 0f);
    }
}
