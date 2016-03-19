using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ChainController : MonoBehaviour {
    public Text chainLabel;
    public RectTransform chainTransformer;
    public int chainLevel;
    public int chainLevelAcum;
    public int chainPoints;
    public bool activeChain;
    public int chainComboFactor = 1;
    public float chainFactor;
    public float chainFactorAcum = 1;
    bool firstEntry;

    public void ResetChain()
    {
        chainPoints = 0;
        chainLevel = 0;
        chainFactor = 0;
        chainLevelAcum = 0;
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
    public bool UpdateChain(FeedbackStateType stateType, int points)
    {
        if(points <= 0)
        {
            return true;
        }
        if (!activeChain)
        {
            InitChain();
        }
        
        if (!firstEntry)
        {
            firstEntry = true;
            return true;
        }
        chainPoints += points;
        switch (stateType)
        {
            case FeedbackStateType.MISS:
                break;
            case FeedbackStateType.GOOD:
                break;
            case FeedbackStateType.GREAT:
                chainLevel++;
                break;
            case FeedbackStateType.BAD:
                break;
            case FeedbackStateType.PERFECT:
                chainLevel++;
                break;
            case FeedbackStateType.TOLATE:
                break;
            case FeedbackStateType.TOEARLY:
                break;
            default:
                break;
        }
        chainLevelAcum++;
        if (chainLevelAcum >= chainComboFactor)
        {
            chainLevelAcum = 0;
            chainFactor += chainFactorAcum;
        }
        updateChainLabel();
        return chainLevel <= 0;
    }

    private void updateChainLabel()
    {
        if(chainLevel > 0)
        {
            chainTransformer.gameObject.SetActive(true);
            chainLabel.gameObject.SetActive(true);
        }

        chainTransformer.transform.DOScale(new Vector2(1.5f, 1.5f), 0.5f).From();

        //chainLabel.text = chainPoints.ToString() + "pts. X "+ chainFactor.ToString();
        chainLabel.text = "X "+ chainFactor.ToString();
    }

    internal int FinishChain(GameObject particlePrefab, RectTransform labelDestiny, RectTransform parent)
    {

        int finishedChainPoints = (int)chainFactor;//(int)Math.Ceiling(chainLevel * chainFactor);
        //print(finishedChainPoints + "= "+ chainLevel + " x " + chainFactor);
        


        for (int i = 0; i < chainFactor; i++)
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


        ResetChain();


        return finishedChainPoints;
    }
    
}
