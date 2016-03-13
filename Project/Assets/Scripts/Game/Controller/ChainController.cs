using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChainController : MonoBehaviour {
    public Text chainLabel;
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
        print(chainLevelAcum + " = " + chainComboFactor);
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
            chainLabel.gameObject.SetActive(true);
        }

        chainLabel.text = chainPoints.ToString() + "pts. X "+ chainFactor.ToString();
    }

    internal int FinishChain()
    {

        int finishedChainPoints = (int)Math.Ceiling(chainLevel * chainFactor);
        //print(finishedChainPoints + "= "+ chainLevel + " x " + chainFactor);
        ResetChain();

        return finishedChainPoints;
    }
    
}
