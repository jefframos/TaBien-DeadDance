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
    public List<AnimationData> standardAnimationData;
    public List<AnimationData> badAnimationData;
    public List<AnimationData> goodAnimationData;
    public List<AnimationData> perfectAnimationData;
    private AnimationData oldAnimation;
    private AnimationData currentAnimation;

    //public Animator headAnimator;
    public Animator bodyAnimator;

    public Vector3 standardLeftArm;
    public AudioSource sourceBreath;
    public AudioSource sourceActions;
    public List<AudioClip> audioActionList;
    private bool started;
    // Use this for initialization
    void Awake()
    {
        started = false;
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
        
        //if (currentAnimation != null && !bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation.label))
        if (currentAnimation != null && bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
        {
            changeAnimation(ChainController.ChainFinishedType.GOOD);            
        }

    }

    private void changeAnimation(ChainController.ChainFinishedType type)
    {
        
        List <AnimationData> tempAnimations = perfectAnimationData;
        switch (type)
        {
            case ChainController.ChainFinishedType.PERFECT:
                tempAnimations = perfectAnimationData;
                break;
            case ChainController.ChainFinishedType.GOOD:
                tempAnimations = goodAnimationData;

                break;
            case ChainController.ChainFinishedType.BAD:
                tempAnimations = badAnimationData;

                break;
            default:
                break;
        }
        int counter = 0;
        if (tempAnimations.Count == 1)
        {
            currentAnimation = tempAnimations[0];
        }
        else {
            while (currentAnimation.label == oldAnimation.label)// || counter < 20)
            {
                int rnd = UnityEngine.Random.Range(0, tempAnimations.Count);
                currentAnimation = tempAnimations[rnd];
                counter++;
            }
        }




        oldAnimation = currentAnimation;
        bodyAnimator.CrossFade(currentAnimation.label, 0.2f);
        sourceActions.PlayOneShot(audioActionList[UnityEngine.Random.Range(0, (audioActionList.Count))]);
    }

    internal void updatePart(Vector3 tempV3)
    {

    }

    internal void updateLevel()
    {
        //bodyAnimator.CrossFade("Standard1", 0.5f);
    }

    internal void Init()
    {
        started = true;
    }

    internal void Reset()
    {
        
        currentAnimation = standardAnimationData[0];
        oldAnimation = currentAnimation;
        //bodyAnimator.Play(currentAnimation.label, -1, 0f);
        bodyAnimator.CrossFade(currentAnimation.label, 0.2f);
    }

    internal void GameOver()
    {
        started = false;
        bodyAnimator.CrossFade("Lost1", 0.2f);
    }

    internal void SetAnimation(ChainController.ChainFinishedType finishedType)
    {
        changeAnimation(finishedType);
        
    }
}
