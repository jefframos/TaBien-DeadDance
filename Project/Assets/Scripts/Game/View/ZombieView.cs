using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class ZombieView : MonoBehaviour {

    public RectTransform leftArm;
    public RectTransform rightArm;
    public RectTransform leftLeg;
    public RectTransform rightLeg;

    [Serializable]
    public class AnimationData
    {
        public string label;
        public int level;
    }
    public List<AnimationData> animationData;
    private List<List<AnimationData>> animationList;
    private AnimationData currentAnimation;

    //public Animator headAnimator;
    public Animator bodyAnimator;

    public Vector3 standardLeftArm;

    private bool started;
    // Use this for initialization
    void Awake()
    {
        standardLeftArm = leftArm.localPosition;
        started = false;
        //animationList = new List<List<AnimationData>>();
        //foreach (AnimationData item in animationData)
        //{

        //}
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void Beat()
    {
        if (!bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation.label))
        {
            print(currentAnimation.label);
            int rnd = UnityEngine.Random.Range(1, animationData.Count);
            currentAnimation = animationData[rnd];
            bodyAnimator.CrossFade(currentAnimation.label, 0.2f);
        }

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
        currentAnimation = animationData[1];
        bodyAnimator.Play(currentAnimation.label, -1, 0f);
    }
}
