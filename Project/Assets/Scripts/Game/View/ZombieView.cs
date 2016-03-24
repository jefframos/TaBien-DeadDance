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
    private AnimationData oldAnimation;
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
        if (!started)
        {
            return;
        }
        
    }

    internal void Beat()
    {
        if (!started)
        {
            return;
        }
        if (currentAnimation != null && !bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation.label))
        {
            int counter = 0;
            
            while (currentAnimation.label == oldAnimation.label)// || counter < 20)
                {
                int rnd = UnityEngine.Random.Range(1, animationData.Count);
                currentAnimation = animationData[rnd];
                counter++;
            }
            oldAnimation = currentAnimation;
            bodyAnimator.CrossFade(currentAnimation.label, 0.2f);
        }

    }

    internal void updatePart(Vector3 tempV3)
    {

    }

    internal void updateLevel()
    {
        //bodyAnimator.CrossFade("Standard1", 0.5f);
    }

    internal void Reset()
    {
        started = true;
        currentAnimation = animationData[1];
        oldAnimation = currentAnimation;
        bodyAnimator.Play(currentAnimation.label, -1, 0f);
    }

    internal void GameOver()
    {
        started = false;
        bodyAnimator.CrossFade("Lost1", 0.2f);
    }
}
