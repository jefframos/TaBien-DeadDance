using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {
    
    public List<WaveModel> wavesList;
    public List<BeatterView> beatterList;

    private List<ActionButtonView> _actionList;

    public GameObject actionButtonPrefab;
    public GameObject particlePrefab;

    public RectTransform actionArea;
    public RectTransform particlesContainer;

    public int testWave = -1;
    public int currentWave = 1;

    public AudioController audioController;
    public Metronome metronome;

    public Text beatCounter;


    public int points;
    public Text pointsLabel;
    public RectTransform pointsLabelRect;


    private int currency;
    public Text currencyLabel;
    public RectTransform currencyLabelRect;
    public RectTransform currencyRect;


    public ChainController chainController;

    public ZombieView zombieView;
    public bool initedGame;

    private List<ActionButtonView> buttonWave;
    private float _beatCounter = 0;
    private int _beatAcum = 0;

    public float levelGauge;
    public float maxGauge;
    public float levelGaugeDecress;
    public Text levelGaugeText;
    public LevelGaugeView levelGaugeView;


    [Serializable]
    public class GaugeValues
    {
        public float miss = 0; 
        public float good = 0; 
        public float great = 0; 
        public float perfect = 0; 
        public float tolate = 0;
        public float toearly = 0;
    }
    public GaugeValues gaugeValues;

    public GameObject middleHUD;
    public Button playButton;

    public AudioSource audioSourceAmbient;
    public CountdownController countdownController;
    // Use this for initialization
    public void ResetWaves()
    {
        wavesList = new List<WaveModel>();
    }
    public void AddWave(WaveModel wave)
    {
        wavesList.Add(wave);
    }
    void Start () {
        //ResetWaves();
        //WaveModel tempWave = new WaveModel();
        //ActionButtonModel tempActionModel = new ActionButtonModel();
        //tempWave.totalBeats = 20;
        //tempWave.actionList = new List<ActionButtonModel>();
        initedGame = false;        
        metronome.beatCallback = BeatCallback;
        chainController.ResetChain();
        middleHUD.SetActive(true);
        audioSourceAmbient.volume = 0.1f;
        countdownController.Hide();
        //InitGame();
    }
    public void InitGame()
    {
        middleHUD.SetActive(false);
        audioSourceAmbient.DOFade(0.25f, 1f);
        getWave();
        _beatAcum = 0;
        _beatCounter = -12;
        points = 0;
        audioController.InitAudioController();
        chainController.ResetChain();
        initedGame = true;
        currency = GetCurrency();
        levelGauge = maxGauge / 2;
        _actionList = new List<ActionButtonView>();
        zombieView.Reset();
        countdownController.Reset(3);

    }

    private int GetCurrency()
    {
        return 0;
    }

    private void BeatCallback()
    {
        if (!initedGame)
        {
            return;
        }
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
            if (_beatCounter < 0)
            {
                if (countdownController.started)
                {
                    if (Mathf.Abs(_beatCounter) % 3 == 0)
                    {

                        countdownController.UpdateCounter((int)Mathf.Abs(_beatCounter) / 3);
                    }
                }
            }
            else
            {
                countdownController.Finish();
            }
        }
        
        updateLevelGauge();
        beatCounter.text = Mathf.Floor(_beatCounter).ToString();
    }

    private void gameOver()
    {
        if (!initedGame)
        {
            return;
        }
        audioSourceAmbient.DOFade(0.1f, 1f);
        middleHUD.SetActive(true);
        initedGame = false;
        chainController.ResetChain();
        audioController.Reset();
        zombieView.GameOver();
        foreach (ActionButtonView actionView in _actionList)
        {
            actionView.ForceDestroy();            
        }
        _actionList = new List<ActionButtonView>();
    }
    private void updateLevelGauge()
    {
        if(levelGauge < 0)
        {
            levelGauge = 0;
        }
        if (levelGauge > maxGauge)
        {
            levelGauge = maxGauge;
        }
        levelGaugeText.text = Math.Ceiling(levelGauge).ToString();
        levelGaugeView.UpdateBar(levelGauge / 100);

        if(levelGauge <= 0)
        {
            gameOver();
        }
    }

    // Update is called once per frame
    void Update () {
        
        if (!audioController.audioSource.isPlaying || !initedGame)
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
            foreach (ActionButtonModel model in wavesList[currentWave].actionList)
            {
                model.placed = false;
            }
            getWave();
        }
        
    }
    private void breakWave()
    {
        foreach (ActionButtonView actionView in _actionList)
        {
            actionView.ForceDestroy();
        }
        _actionList = new List<ActionButtonView>();
        foreach (ActionButtonModel model in wavesList[currentWave].actionList)
        {
            model.placed = false;
        }
        getWave();
    }
    private void getWave()
    {
        _beatCounter = -8;
        if (testWave < 0)
        {
            currentWave = UnityEngine.Random.Range(0, wavesList.Count);
        }
        else
        {
            currentWave = testWave;
        }
        chainController.ActionsInWave = (wavesList[currentWave].actionList.Count);
        chainController.ResetChain();
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
        actionView.chainController = chainController;
        actionView.Build(model);
        actionView.gameObject.SetActive(true);
        _actionList.Add(actionView);

        actionView.finishCallback = (() =>
        {
            _actionList.Remove(actionView);
            int actionPoints = 0;
            int goldenBrain = 0;
            float gaugeAccum = 0;
            if (actionView.model.actionType == ActionType.SPECIAL)
            {
                if (actionView.currentFeedbackState == FeedbackStateType.MISS)
                {

                }
                else{
                    goldenBrain = 1;
                }
            }
            else {
                switch (actionView.currentFeedbackState)
                {
                    case FeedbackStateType.MISS:
                        //zombieView.updatePart(tempV3);
                        gaugeAccum = gaugeValues.miss;
                        break;
                    case FeedbackStateType.GOOD:
                        gaugeAccum = gaugeValues.good;
                        actionPoints = 1;
                        break;
                    case FeedbackStateType.GREAT:
                        gaugeAccum = gaugeValues.great;
                        actionPoints = 2;
                        break;
                    case FeedbackStateType.BAD:
                        gaugeAccum = gaugeValues.miss;
                        break;
                    case FeedbackStateType.PERFECT:
                        gaugeAccum = gaugeValues.perfect;
                        actionPoints = 3;
                        break;
                    case FeedbackStateType.TOLATE:
                        gaugeAccum = gaugeValues.tolate;
                        break;
                    case FeedbackStateType.TOEARLY:
                        gaugeAccum = gaugeValues.toearly;
                        break;
                    default:
                        break;
                }
            }

            ChainActionType chainActionType = chainController.UpdateChain(actionView.currentFeedbackState);

            ChainController.ChainFinishedType finishedType = ChainController.ChainFinishedType.BAD;
            if (chainActionType != ChainActionType.STANDARD)
            {
                float finishedFactor = chainController.chainLevel / chainController.ActionsInWave;
                
                if (finishedFactor >= 0.9)
                {
                    finishedType = ChainController.ChainFinishedType.PERFECT;

                }
                else if (finishedFactor >= 0.6)
                {
                    finishedType = ChainController.ChainFinishedType.GOOD;
                }

                if (chainActionType == ChainActionType.FINISHED)
                {
                    chainController.FinishChain(finishedType);
                }
                else if (chainActionType == ChainActionType.BROKE)
                {
                    //finishedType = ChainController.ChainFinishedType.BAD;
                    chainController.BreakChain(finishedType);
                    breakWave();
                }


                zombieView.SetAnimation(finishedType);
            }

            


            levelGauge += gaugeAccum;
            //levelGauge += actionPoints * 2;
            updateLevelGauge();

            if (actionPoints + goldenBrain > 0)
            {
                for (int i = 0; i < actionPoints + goldenBrain; i++)
                {
                    GameObject tempParticle;
                    tempParticle = (GameObject)Instantiate(particlePrefab, new Vector3(), Quaternion.identity);
                    tempParticle.transform.SetParent(particlesContainer.transform, false);
                    ActionParticleView particleView = tempParticle.GetComponent<ActionParticleView>();

                    float distance = Vector2.Distance(new Vector2(pointsLabelRect.position.x, pointsLabelRect.position.y), new Vector2(tempV3.x, tempV3.y));
                    
                    particleView.time = Screen.height / distance * 0.8f;
                    particleView.delay = 0.1f + i * 0.2f;
                    particleView.destiny = pointsLabelRect.position;
                    particleView.rectTarget = pointsLabelRect;

                    float newPosX = tempV3.x;
                    float newPosY = tempV3.y;
                    if (goldenBrain > 0)
                    {
                        particleView.destiny = currencyRect.position;
                        particleView.rectTarget = currencyRect;
                        particleView.endTweenCallback = (() => { updateCurrency(1); });
                        particleView.delay = 0.7f + i * 0.2f;
                        particleView.time *= 1.2f;
                        //colocar aqui os destinos certos dos cerebros
                    }
                    else
                    {
                        particleView.endTweenCallback = (() => { updatePoints(1); });
                        newPosX = tempV3.x + UnityEngine.Random.Range(-actionView.innerContent.rect.width / 2, actionView.innerContent.rect.width / 2);
                        newPosY = tempV3.y + UnityEngine.Random.Range(-actionView.innerContent.rect.height / 2, actionView.innerContent.rect.height / 2);
                    }                    
                    particleView.initPos = new Vector3(newPosX, newPosY, 0);                   

                    particleView.color = actionView.currentState.color; 
                    particleView.Build(goldenBrain > 0, actionPoints + goldenBrain);                    
                }                
            }
        });
        model.placed = true;
    }
    private void updateCurrency(int currencyPoints)
    {
        currency += currencyPoints;
        updateCurrencyLabel();
    }
    private void updatePoints(int actionPoints)
    {
        //print(actionPoints);
        points += actionPoints;
        updatePointsLabel();

        //if(points > 50)
        //{
        //    //audioController.IncreaseBPM();
        //    zombieView.updateLevel();
        //}
    }
    private void updatePointsLabel()
    {
        pointsLabel.text = points.ToString();
    }
    private void updateCurrencyLabel()
    {
        currencyLabel.text = currency.ToString();
    }
}
