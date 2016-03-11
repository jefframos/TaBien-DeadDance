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
    // Use this for initialization
    void Awake()
    {
        standardLeftArm = leftArm.localPosition;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Beat()
    {
        //headAnimator.Play("HeadStandard", -1, 0f);
        //bodyAnimator.Play("BodyStandard", -1, 0f);

    }

    internal void updatePart(Vector3 tempV3)
    {
        //leftArm.DOKill(); 
        //leftArm.DOLocalMove(tempV3, 0.2f);
        //leftArm.DOLocalMove(standardLeftArm, 0.2f).SetDelay(0.25f);
        //print(tempV3);
    }
}
