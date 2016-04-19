using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class ZombieView : MonoBehaviour {

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
    public AudioSource sourceChangeParts;
    public AudioClip audioChangeParts;
    public List<AudioClip> audioActionList;
    private bool started;

    private HeadParts headParts;
    public string[] monsterPath;

    public Transform headContainer;
    public CanvasRenderer HeadCanvasRenderer;

    private SpriteRenderer[] rendererList;
    
    void Awake()
    {

        


        started = false;
        
        rendererList = GetComponentsInChildren<SpriteRenderer>();

        headParts = new HeadParts(rendererList);
        headParts.pathID = 0;

    }
    public void UpdateHead(int side)
    {
        if (headParts.working)
        {
            return;
        }

        headParts.pathID += side;
        if(headParts.pathID >= monsterPath.Length)
        {
            headParts.pathID = 0;
        }else if (headParts.pathID < 0)
        {
            headParts.pathID = monsterPath.Length - 1;
        }

        headParts.working = true;
        
        headContainer.localScale = new Vector3(1f, 1f, 1f);

        //headParts.UpdateParts(rendererList, monsterPath[headParts.pathID]);
        headParts.FadeOut(0.2f,0.3f);

        headContainer.DOScale(new Vector3(1.4f, 1.4f, 4f), 0.4f).OnComplete(() =>
        {
            
            headParts.working = false;

            headParts.UpdateParts(monsterPath[headParts.pathID]);
            sourceChangeParts.PlayOneShot(audioChangeParts);
            sourceActions.PlayOneShot(audioActionList[UnityEngine.Random.Range(0, (audioActionList.Count))]);

            headParts.ForceFadeIn(0.2f, 0.45f);
            headContainer.localScale = new Vector3(0.3f, 0.3f,1f);
            headContainer.DOScale(new Vector3(1f, 1f, 1f), 1f).SetDelay(0.5f).SetEase(Ease.OutElastic).OnComplete(() =>
            {

            });
        }).SetEase(Ease.InBack);

        print(headContainer.localScale);
    }
    // Update is called once per frame
    void Update () {
        if (!started)
        {
            return;
        }

        bodyAnimator.speed = MadnessFactor.Multiplier;
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
            changeAnimation(ChainFinishedType.GOOD);            
        }

    }

    private void changeAnimation(ChainFinishedType type)
    {
        //return;
        List <AnimationData> tempAnimations = perfectAnimationData;
        switch (type)
        {
            case ChainFinishedType.PERFECT:
                tempAnimations = perfectAnimationData;
                break;
            case ChainFinishedType.GOOD:
                tempAnimations = goodAnimationData;

                break;
            case ChainFinishedType.BAD:
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
        //bodyAnimator.CrossFade(currentAnimation.label, 0.2f);
    }

    internal void GameOver()
    {
        started = false;
        //bodyAnimator.CrossFade("Lost1", 0.2f);
    }

    internal void SetAnimation(ChainFinishedType finishedType)
    {
        changeAnimation(finishedType);
        
    }
}
