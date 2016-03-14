using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {

    public List<WaveModel> wavesList;
    public List<BeatterView> beatterList;

    public GameObject actionButtonPrefab;
    public GameObject particlePrefab;

    public RectTransform actionArea;
    public RectTransform particlesContainer;

    public int testWave = -1;
    public int currentWave = 1;

    public AudioController audioController;
    public Metronome metronome;

    public Text beatCounter;
    public Text pointsLabel;
    public RectTransform pointsLabelRect;

    public int points;
    public ChainController chainController;

    public ZombieView zombieView;
    public bool initedGame;

    private List<ActionButtonView> buttonWave;
    private float _beatCounter = 0;
    private int _beatAcum = 0;    
    // Use this for initialization
    void Start () {
        initedGame = false;        
        metronome.beatCallback = BeatCallback;
        InitGame();
    }
    public void InitGame()
    {
        getWave();
        audioController.InitAudioController();
        chainController.ResetChain();
        initedGame = true;
    }
    private void BeatCallback()
    {
        _beatCounter += 0.25f;
        _beatAcum++;

        if(_beatAcum >= 4)
        {
            zombieView.Beat();
            _beatAcum = 0;
            foreach (BeatterView item in beatterList)
            {
                item.Beat();
            }
        }

        beatCounter.text = Mathf.Floor(_beatCounter).ToString();
    }

    // Update is called once per frame
    void Update () {
        
        if (!audioController.audioSource.isPlaying)
        {
            return;
        }
        metronome.BPM = audioController.currentAudioLoop.BPM;
        foreach (ActionButtonModel model in wavesList[currentWave].actionList)
        {
            if(!model.placed && _beatCounter >= ((float)(model.quarterBeatAppear - model.quarterBeatToTap)/4))
            {
                addAction(model);
            }
        }

        if (_beatCounter > wavesList[currentWave].totalBeats)
        {
            _beatCounter = 0;
            foreach (ActionButtonModel model in wavesList[currentWave].actionList)
            {
                model.placed = false;
            }
            getWave();
        }
        
    }
    private void getWave()
    {
        if (testWave < 0)
        {
            currentWave = UnityEngine.Random.Range(0, wavesList.Count);
        }
        else
        {
            currentWave = testWave;
        }
    }
    private void addAction(ActionButtonModel model)
    {

        Vector3 tempV3 = new Vector3();

        int rndX = (int)model.gridPosition.x;// UnityEngine.Random.Range(0, (int)grid.x);
        int rndY = (int)model.gridPosition.y;//UnityEngine.Random.Range(0, (int)grid.y);
        
       
        float tempX = (actionArea.rect.max.x - actionArea.rect.min.x) / wavesList[currentWave].grid.x;
        float tempY = (actionArea.rect.max.y - actionArea.rect.min.y) / wavesList[currentWave].grid.y;

        tempV3.x = rndX * tempX + actionArea.rect.min.x;
        tempV3.y = rndY * tempY + actionArea.rect.min.y;

        GameObject tempObject = (GameObject)Instantiate(actionButtonPrefab, tempV3, Quaternion.identity);
        tempObject.transform.SetParent(actionArea.transform, false);
        ActionButtonView actionView = tempObject.GetComponent<ActionButtonView>();
        actionView.Build(model);
        actionView.gameObject.SetActive(true);
        actionView.finishCallback = (() =>
        {
            int actionPoints = 0;
                switch (actionView.currentFeedbackState)
                {
                    case FeedbackStateType.MISS:
                        applyChainPoints();
                        zombieView.updatePart(tempV3);
                        break;
                        case FeedbackStateType.GOOD:
                        actionPoints = 1;
                        break;
                    case FeedbackStateType.GREAT:
                        actionPoints = 2;
                        break;
                    case FeedbackStateType.BAD:
                        applyChainPoints();
                        break;
                    case FeedbackStateType.PERFECT:
                        actionPoints = 3;
                        break;
                    case FeedbackStateType.TOLATE:
                        applyChainPoints();
                        break;
                    case FeedbackStateType.TOEARLY:
                        applyChainPoints();
                        break;
                    default:
                        break;
                }
            bool toUpdatePoints = false;
            toUpdatePoints = chainController.UpdateChain(actionView.currentFeedbackState, actionPoints);
            //if (toUpdatePoints)
            //{
            //    updatePoints(actionPoints);
            //}

            updatePoints(actionPoints);

            if (actionPoints > 0)
            {
                for (int i = 0; i < actionPoints; i++)
                {
                    GameObject tempParticle = (GameObject)Instantiate(particlePrefab, new Vector3(), Quaternion.identity);
                    tempParticle.transform.SetParent(particlesContainer.transform, false);
                    ActionParticleView particleView = tempParticle.GetComponent<ActionParticleView>();

                    float distance = Vector2.Distance(new Vector2(pointsLabelRect.position.x, pointsLabelRect.position.y), new Vector2(tempV3.x, tempV3.y));
                    particleView.color = actionView.currentState.color;

                    float newPosX = tempV3.x + UnityEngine.Random.Range(-actionView.innerContent.rect.width / 2, actionView.innerContent.rect.width / 2);
                    float newPosY = tempV3.y + UnityEngine.Random.Range(-actionView.innerContent.rect.height / 2, actionView.innerContent.rect.height / 2);
                    particleView.initPos = new Vector3(newPosX, newPosY, 0);

                    particleView.time = Screen.height / distance * 0.8f;
                    particleView.delay = 0.1f + i*0.2f;
                    particleView.destiny = pointsLabelRect.position;
                    particleView.rectTarget = pointsLabelRect;
                    particleView.Build();
                }                
            }
        });
        model.placed = true;       
    }
    private void updatePoints(int actionPoints)
    {
        points += actionPoints;
        updatePointsLabel();

        if(points > 50)
        {
            audioController.IncreaseBPM();
            zombieView.updateLevel();
        }
    }
    private void applyChainPoints()
    {
        int chainPoints = chainController.FinishChain();
        points += chainPoints;
        updatePointsLabel();
    }
    private void updatePointsLabel()
    {
        pointsLabel.text = points.ToString();
    }
    
}
