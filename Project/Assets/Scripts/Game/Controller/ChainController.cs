using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class ChainController : MonoBehaviour {    
    public Text chainLabel;
    public RectTransform chainTransformer;
    public float chainLevel;
    public float chainCounter;
    //private float chainLevelAcum;
    public float maxPitch;
    private float pitchAcum;
    public float currentPitch;

    public bool activeChain;
    public int chainComboFactor = 1;
    public float chainFactor;
    public float chainFactorAcum = 0.1f;

    private int actionsInWave;
    public AudioClip perfectWave;
    public AudioClip goodWave;
    public AudioClip badWave;
    public AudioSource audioSource;
    public AudioSource audienceSource;
    public AudioSource feedbackSource;
    public List<AudioClip> greatChainSounds;
    public List<AudioClip> goodChainSounds;
    public List<AudioClip> badChainSounds;
    public float audienceVolume = 0.5f;

    public Text resultLabel;
    public RectTransform container;

    public int ActionsInWave {
        get{
            return actionsInWave;
        }
        set {
            actionsInWave = value;
            pitchAcum = (maxPitch - 1) / actionsInWave;
            //chainLevelAcum = 1;
        }
    }

    public void ResetChain()
    {
        chainCounter = 0;
        chainLevel = 0;
        chainFactor = 0;
        currentPitch = 1;
        activeChain = false;

        chainLabel.gameObject.SetActive(false);
        chainTransformer.gameObject.SetActive(false);
    }
    public void InitChain()
    {
        ResetChain();
        activeChain = true;
    }
    public ChainActionType UpdateChain(FeedbackStateType stateType)
    {

        if (!activeChain)
        {
            InitChain();
        }
        bool finished = false;
        bool broke = false;
        switch (stateType)
        {
            case FeedbackStateType.MISS:
                //broke = true;
                break;
            case FeedbackStateType.GOOD:
                //chainLevel += chainLevelAcum;
                chainLevel += 1;
                break;
            case FeedbackStateType.GREAT:
                //chainLevel += chainLevelAcum;
                chainLevel += 2;
                break;
            case FeedbackStateType.BAD:
                //broke = true;
                break;
            case FeedbackStateType.PERFECT:
                //chainLevel += chainLevelAcum;
                chainLevel += 3;
                break;
            case FeedbackStateType.TOLATE:
                //broke = true;
                break;
            case FeedbackStateType.TOEARLY:
                //broke = true;
                break;
            default:
                break;
        }
        //updateChainLabel();
        chainCounter++;
        currentPitch += pitchAcum;
        if (!finished)
        {
            finished = actionsInWave <= chainCounter;
        }
        return broke? ChainActionType .BROKE: finished ? ChainActionType.FINISHED : ChainActionType.STANDARD;
    }

    private void updateChainLabel()
    {
        if(chainLevel > 1)
        {
            chainTransformer.gameObject.SetActive(true);
            chainLabel.gameObject.SetActive(true);
        }

        chainTransformer.transform.DOScale(new Vector2(1.5f, 1.5f), 0.5f).From();
        chainLabel.text = "X "+ chainLevel.ToString();
    }
    internal float FinishChain(ChainFinishedType finishedType)
    {
        float returnChain = chainLevel;
        ResetChain();
        playSound(finishedType);
        //audienceSource.DOFade(0f, 0.5f).SetDelay(0.55f);
        ShowResult(finishedType);
        return returnChain;
    }    
    internal void ShowResult(ChainFinishedType finishedType)
    {
        resultLabel.gameObject.SetActive(true);
        container.transform.localScale = new Vector3(0.5f, 0.5f);
        CanvasGroup canvasGroup = container.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        resultLabel.text = finishedType.ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(container.DOScale(1f, 1f).SetEase(Ease.OutElastic));
        seq.Append(container.DOScale(1.5f, 0.8f).SetEase(Ease.InBack));
        seq.Insert(1, canvasGroup.DOFade(0, 0.3f).SetDelay(0.5f));

    }
    internal void BreakChain(ChainFinishedType finishedType)
    {
        playSound(finishedType);
    }
    internal void playSound(ChainFinishedType finishedType)
    {
        audioSource.gameObject.SetActive(true);
        audienceSource.gameObject.SetActive(true);
        feedbackSource.gameObject.SetActive(true);

        AudioClip tempAudioClip;
        switch (finishedType)
        {
            case ChainFinishedType.PERFECT:
                feedbackSource.PlayOneShot(perfectWave, 0.6f);
                tempAudioClip = greatChainSounds[UnityEngine.Random.Range(0, greatChainSounds.Count)];
               
                break;
            case ChainFinishedType.GOOD:
                feedbackSource.PlayOneShot(goodWave, 0.8f);
                tempAudioClip = goodChainSounds[UnityEngine.Random.Range(0, goodChainSounds.Count)];
                
                break;
            case ChainFinishedType.BAD:
                feedbackSource.PlayOneShot(badWave, 1f);
                print(badWave);
                tempAudioClip = badChainSounds[UnityEngine.Random.Range(0, badChainSounds.Count)];
                break;
            default:
                tempAudioClip = goodChainSounds[UnityEngine.Random.Range(0, goodChainSounds.Count)];
                break;
        }

        audienceSource.PlayOneShot(tempAudioClip, audienceVolume);
        //audienceSource.DOFade(audienceVolume / 2, 0.3f);
        //audienceSource.volume = 0;
        //audienceSource.DOFade(0.5f, 0.5f);
    }
}
