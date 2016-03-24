using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ChainController : MonoBehaviour {
    public Text chainLabel;
    public RectTransform chainTransformer;
    public float chainLevel;
    public float chainCounter;
    private float chainLevelAcum;
    public int maxPitch = 6;

    public bool activeChain;
    public int chainComboFactor = 1;
    public float chainFactor;
    public float chainFactorAcum = 0.1f;
    bool firstEntry;
    private int actionsInWave;

    public int ActionsInWave {
        get{
            return actionsInWave;
        }
        set {
            actionsInWave = value;
            chainLevelAcum = actionsInWave / maxPitch;
        }
    }

    public void ResetChain()
    {
        chainLevel = 1;
        chainFactor = 0;
        activeChain = false;
        firstEntry = false;
        chainLabel.gameObject.SetActive(false);
        chainTransformer.gameObject.SetActive(false);
    }
    public void InitChain()
    {
        ResetChain();
        activeChain = true;
    }
    public bool UpdateChain(FeedbackStateType stateType)
    {

        if (!activeChain)
        {
            InitChain();
        }        

        switch (stateType)
        {
            case FeedbackStateType.MISS:
                break;
            case FeedbackStateType.GOOD:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.GREAT:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.BAD:
                break;
            case FeedbackStateType.PERFECT:
                chainLevel += chainLevelAcum;
                break;
            case FeedbackStateType.TOLATE:
                break;
            case FeedbackStateType.TOEARLY:
                break;
            default:
                break;
        }
        updateChainLabel();
        chainCounter++;
        
        return maxPitch <= chainLevel;
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
        
        return returnChain;
    }

}
