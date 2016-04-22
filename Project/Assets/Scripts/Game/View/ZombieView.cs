using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class ZombieView : MonoBehaviour {

    public ZombiePartsController HatData;
    public ZombiePartsController HeadData;
    public ZombiePartsController AcessoryData;
    public ZombiePartsController BodyData;
    public ZombiePartsController PantsData;

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
    
    public string[] monsterPath;

    public Transform headContainer;

    internal void Show()
    {
        HeadData.ForceFadeIn(0.2f, 0.45f);
        BodyData.ForceFadeIn(0.2f, 0.45f);
        PantsData.ForceFadeIn(0.2f, 0.45f);
    }

    internal void Hide()
    {
        HeadData.ForceFadeOut(0, 0);
        BodyData.ForceFadeOut(0, 0);
        PantsData.ForceFadeOut(0, 0);
    }

    public CanvasRenderer HeadCanvasRenderer;

    private SpriteRenderer[] rendererList;
    private bool built = false;
    void Awake()
    {
        

    }
    public void Build()
    {
        if (built)
        {
            print("ALREADY BUILD");
            return;
        }
        built = true;
        rendererList = GetComponentsInChildren<SpriteRenderer>();

        HeadData = new ZombiePartsController(rendererList, new List<string>(new string[] {
            "eye1_closed",
            "eye2_closed",
            "ear_left",
            "ear_right",
            "hair",
            "eye1",
            "eye2",
            "mouth",
            "pupila1",
            "pupila2",
            "head",
            "hat",
            "acessory",
            "queixo"
        }));

        AcessoryData = new ZombiePartsController(rendererList, new List<string>(new string[] {
            "acessory"
        }));

        HatData = new ZombiePartsController(rendererList, new List<string>(new string[] {
            "hat"
        }));

        PantsData = new ZombiePartsController(rendererList, new List<string>(new string[] {
            "ass",
            "coxa_left",
            "coxa_right",
            "feet_left",
            "feet_right",
            "leg_left",
            "leg_right"
        }));

        BodyData = new ZombiePartsController(rendererList, new List<string>(new string[] {
            "arm_left",
            "arm_right",
            "hand_left",
            "hand_right",
            "hand_left_closed",
            "hand_right_closed",
            "hand_left_open",
            "hand_right_open",
            "shoulder_left",
            "shoulder_right",
            "body"
        }));
        started = false;
    }
    internal void UpdatePart(PartsModel partModel, bool force = false)
    {
        //print(partModel.Path);
        if (!force)
        {
            sourceChangeParts.PlayOneShot(audioChangeParts);
        }
        switch (partModel.PartType)
        {
            case ShopSectionType.HEAD:
                if (force)
                {
                    HeadData.UpdateParts(partModel);
                    HeadData.ForceFadeIn(0.2f, 0.45f);
                }
                else {
                    HeadData.working = true;
                    headContainer.localScale = new Vector3(1f, 1f, 1f);

                    //headParts.UpdateParts(rendererList, monsterPath[headParts.pathID]);
                    HeadData.FadeOut(0.2f, 0.3f);

                    headContainer.DOScale(new Vector3(1.4f, 1.4f, 4f), 0.4f).OnComplete(() =>
                    {
                        HeadData.working = false;
                        HeadData.UpdateParts(partModel);

                        sourceActions.PlayOneShot(audioActionList[UnityEngine.Random.Range(0, (audioActionList.Count))]);

                        HeadData.ForceFadeIn(0.2f, 0.45f);
                        headContainer.localScale = new Vector3(0.3f, 0.3f, 1f);
                        headContainer.DOScale(new Vector3(1f, 1f, 1f), 1f).SetDelay(0.5f).SetEase(Ease.OutElastic).OnComplete(() =>
                        {

                        });
                    }).SetEase(Ease.InBack);
                    //HeadData.UpdateParts(partModel);
                }
                break;
            case ShopSectionType.BODY:
                BodyData.UpdateParts(partModel);
                BodyData.ForceFadeIn(0.2f, 0.45f);
                break;
            case ShopSectionType.PANTS:
                PantsData.UpdateParts(partModel);
                PantsData.ForceFadeIn(0.2f, 0.45f);
                break;
            case ShopSectionType.HATS:
                HatData.UpdateParts(partModel);
                HatData.ForceFadeIn(0.2f, 0.45f);
                break;
            case ShopSectionType.ACESSORY:
                AcessoryData.UpdateParts(partModel);
                AcessoryData.ForceFadeIn(0.2f, 0.45f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!built)
        {
            Build();
        }
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
