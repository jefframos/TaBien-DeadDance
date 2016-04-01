using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class ChainController : MonoBehaviour {
    public enum ChainFinishedType { PERFECT, GOOD, BAD};
    public Text chainLabel;
    public RectTransform chainTransformer;
    public float chainLevel;
    public float chainCounter;
    private float chainLevelAcum;
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
    public List<AudioClip> greatChainSounds;
    public List<AudioClip> goodChainSounds;
    public List<AudioClip> badChainSounds;
    public int ActionsInWave {
        get{
            return actionsInWave;
        }
        set {
            actionsInWave = value;
            pitchAcum = (maxPitch - 1) / actionsInWave;
            chainLevelAcum = 1;
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
                broke = true;
                break;
            case FeedbackStateType.GOOD:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.GREAT:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.BAD:
                broke = true;
                break;
            case FeedbackStateType.PERFECT:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.TOLATE:
                broke = true;
                break;
            case FeedbackStateType.TOEARLY:
                broke = true;
                break;
            default:
                break;
        }
        updateChainLabel();
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

        return returnChain;
    }
    internal float FinishChain(GameObject particlePrefab, RectTransform labelDestiny, RectTransform parent)
    {
        for (int i = 0; i < 1; i++)
        {
            
            float newPosX = chainLabel.transform.position.x + chainLabel.transform.parent.transform.position.x + this.transform.parent.transform.position.x;
            float newPosY = chainLabel.transform.position.y + chainLabel.transform.parent.transform.position.y + this.transform.parent.transform.position.y;


            GameObject tempParticle;
            tempParticle = (GameObject)Instantiate(particlePrefab, new Vector3(), Quaternion.identity);
            tempParticle.transform.SetParent(parent.transform, false);
            ActionParticleView particleView = tempParticle.GetComponent<ActionParticleView>();

            float distance = Vector2.Distance(new Vector2(labelDestiny.position.x, labelDestiny.position.y), new Vector2(newPosX , newPosY));



            particleView.time = Screen.height / distance * 0.2f;
            particleView.delay = i * 0.1f;
            particleView.destiny = labelDestiny.position;
            particleView.rectTarget = labelDestiny;            
            particleView.initPos = new Vector3(newPosX, newPosY, 0);            
            particleView.Build(false, 0);
        }

        float returnChain = chainLevel;
        ResetChain();
        print("FINISH CHAIN");
        audioSource.PlayOneShot(perfectWave, 0.5f);
        return returnChain;
    }

    internal void BreakChain(ChainFinishedType finishedType)
    {
        playSound(finishedType);
    }
    internal void playSound(ChainFinishedType finishedType)
    {
        audioSource.gameObject.SetActive(true);
        audienceSource.gameObject.SetActive(true);
        print("CONCERTAR BAD SOUND");
        switch (finishedType)
        {
            case ChainFinishedType.PERFECT:
                audioSource.PlayOneShot(perfectWave, 0.3f);
                audienceSource.PlayOneShot(greatChainSounds[UnityEngine.Random.Range(0, greatChainSounds.Count)], 0.5f);
                break;
            case ChainFinishedType.GOOD:
                audioSource.PlayOneShot(goodWave, 0.2f);
                audienceSource.PlayOneShot(goodChainSounds[UnityEngine.Random.Range(0, goodChainSounds.Count)], 0.5f);
                break;
            case ChainFinishedType.BAD:
                //audioSource.PlayOneShot(badWave, 0.3f);
                print(badChainSounds);
                audienceSource.PlayOneShot(badChainSounds[UnityEngine.Random.Range(0, badChainSounds.Count)], 0.5f);
                break;
            default:
                break;
        }
        //audienceSource.volume = 0;
        //audienceSource.DOFade(0.5f, 0.5f);
    }
}
