using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {

    public GameEffects GameEffects;
    public RectTransform GameContainer;
    public List<WaveModel> WavesList;
    public List<BeatterView> BeatterList;

    private List<ActionButtonView> _actionList;

    public GameObject ActionButtonPrefab;
    public GameObject ParticlePrefab;

    public RectTransform ActionArea;
    public RectTransform ParticlesContainer;

    public int TestWave = -1;
    public int CurrentWave = 1;

    public AudioController AudioController;
    public Metronome Metronome;

    public Text BeatCounterLabel;


    public int Points;
    public Text PointsLabel;
    public RectTransform PointsLabelRect;


    private int currency;
    public Text CurrencyLabel;
    public RectTransform CurrencyLabelRect;
    public RectTransform CurrencyRect;


    public ChainController ChainController;

    public ZombieView Zombie;
    public bool InitedGame;

    private List<ActionButtonView> buttonWave;
    private float _beatCounter = 0;
    private int _beatAcum = 0;

    public float LevelGauge;
    public float MaxGauge;

    public Text LevelGaugeLabel;
    public LevelGaugeView LevelGaugeView;


    [Serializable]
    public class GaugeValuesData
    {
        public float Miss = 0; 
        public float Good = 0; 
        public float Great = 0; 
        public float Perfect = 0; 
        public float ToLate = 0;
        public float ToEarly = 0;
    }
    public GaugeValuesData GaugeValues;
    

    //public AudioSource audioSourceAmbient;
    public CountdownController CountdownController;

    public LifeController LifeController;

    public MainHUDController MainHUDController;

    public Animator MainAnimator;
    public Animator HUDAnimator;
    // Use this for initialization
    public int Life = 3;
    public bool Paused;
    public int CurrentLevel;
    public void ResetWaves()
    {
        WavesList = new List<WaveModel>();
    }
    public void AddWave(WaveModel wave)
    {
        WavesList.Add(wave);
    }
    void Start () {
        //ResetWaves();
        //WaveModel tempWave = new WaveModel();
        //ActionButtonModel tempActionModel = new ActionButtonModel();
        //tempWave.totalBeats = 20;
        //tempWave.actionList = new List<ActionButtonModel>();
        InitedGame = false;        
        Metronome.beatCallback = BeatCallback;
        ChainController.ResetChain();
        //middleHUD.SetActive(true);
        CountdownController.Hide();
        //InitGame();
    }
    public void ToShop()
    {

    }
    public void ToHome()
    {
        //middleHUD.SetActive(true);

        //MainAnimator.gameObject.SetActive(true);
        //MainAnimator.Play("HUDAnimator");
        //MainAnimator.SetTrigger("ToStandard");
    }
    public void InitGame()
    {


        UnpauseGame();

        //middleHUD.SetActive(false);
        //audioSourceAmbient.DOFade(0.25f, 1f);

        _beatAcum = 0;
        _beatCounter = 0;
        Points = 0;
        Life = 1;
        currency = GetCurrency();
        LifeController.Reset();

        //Reset Chain
        ChainController.ResetChain();

        CurrentLevel = 1;

        //Reinit level gauge
        LevelGauge = MaxGauge / 2;

        //Reinit action list
        _actionList = new List<ActionButtonView>();

        //Reset Zombie
        Zombie.Reset();
        CountdownController.Init(3, AfterCountdown, 1.5f);
        updatePoints(Points);

        //Force hide main HUD
        MainHUDController.Hide();

        getWave();
    }

    void AfterCountdown()
    {
        InitedGame = true;
        Zombie.Init();
        AudioController.InitAudioController();

        //MainAnimator.Stop();

        GameEffects.ShakePos(GameContainer,10f);
        GameEffects.ShakeScale(GameContainer);

    }
    private int GetCurrency()
    {
        return 0;
    }

    private void BeatCallback()
    {
        if (!InitedGame || Paused)
        {
            return;
        }
        _beatCounter += 0.25f;
        _beatAcum++;

        if(_beatAcum >= 4)
        {
            Zombie.Beat();
            _beatAcum = 0;
            foreach (BeatterView item in BeatterList)
            {
                item.Beat();
            }            
        }
        
        updateLevelGauge();
        BeatCounterLabel.text = Mathf.Floor(_beatCounter).ToString();
    }

    private void reduceLife()
    {
        //GameContainer.DOShakePosition(2f, 100f, 100);
        //GameContainer.DOShakeScale(2f, 2f);
        Life--;
        LifeController.UpdateHearthList(Life);
        if (Life <= 0)
        {
            Invoke("preGameOver", 2f);
            //gameOver();
        }
    }
    public void AcceptDeal()
    {
        AddLife(1);
        MainHUDController.AcceptMoreLife();
        _beatCounter = -1;
        getWave();
    }

    public void RejectDeal()
    {
        gameOver();
        MainHUDController.ShowEndGame();
    }

    public void AddLife(int value = 1)
    {
        Life += value;
        LifeController.UpdateHearthList(Life);
        UnpauseGame();

    }
    public void UnpauseGame()
    {
        Paused = false;
    }
    public void PauseGame()
    {
        Paused = true;
    }
    private void preGameOver()
    {
        PauseGame();
        MainHUDController.ShowPreEndGame();
    }
    private void gameOver()
    {
        if (!InitedGame)
        {
            return;
        }
        //audioSourceAmbient.DOFade(0.1f, 1f);
        //middleHUD.SetActive(true);
        InitedGame = false;
        //chainController.ResetChain();
        AudioController.Reset();
        Zombie.GameOver();
        foreach (ActionButtonView actionView in _actionList)
        {
            actionView.ForceDestroy();            
        }
        _actionList = new List<ActionButtonView>();
        Points = 0;
        updatePointsLabel();

        print("GameOver");
    }
    private void updateLevelGauge()
    {
        if(LevelGauge < 0)
        {
            LevelGauge = 0;
        }
        if (LevelGauge > MaxGauge)
        {
            LevelGauge = MaxGauge;
        }
        LevelGaugeLabel.text = Math.Ceiling(LevelGauge).ToString();
        LevelGaugeView.UpdateBar(LevelGauge / 100);
    }

    // Update is called once per frame
    void Update () {
        if (Paused)
        {
            return;
        }
        if (!AudioController.audioSource.isPlaying || !InitedGame)
        {
            
            return;
        }
        Metronome.BPM = AudioController.currentAudioLoop.BPM;
        foreach (ActionButtonModel model in WavesList[CurrentWave].actionList)
        {
            if (!model.placed && _beatCounter >= ((float)(model.quarterBeatAppear - model.quarterBeatToTap)/4))
            {
                addAction(model);
            }
        }

        if (_beatCounter - 1 > WavesList[CurrentWave].totalBeats)
        {
            foreach (ActionButtonModel model in WavesList[CurrentWave].actionList)
            {
                model.placed = false;
            }

            getWave();
        }
        
    }
    //private void breakWave()
    //{
    //    foreach (ActionButtonView actionView in _actionList)
    //    {
    //        actionView.ForceDestroy();
    //    }
    //    _actionList = new List<ActionButtonView>();
    //    foreach (ActionButtonModel model in wavesList[currentWave].actionList)
    //    {
    //        model.placed = false;
    //    }
    //    getWave();
    //}
    private void getWave()
    {
        _beatCounter = 0;
        if (TestWave < 0)
        {
            CurrentWave = UnityEngine.Random.Range(0, WavesList.Count);
        }
        else
        {
            CurrentWave = TestWave;
        }
        foreach (ActionButtonModel model in WavesList[CurrentWave].actionList)
        {
            model.placed = false;
        }
        ChainController.ActionsInWave = (WavesList[CurrentWave].actionList.Count);
        //chainController.ResetChain();
    }
    private void addAction(ActionButtonModel model)
    {
       

        ChainController.PlacedActions++;
        Vector3 tempV3 = new Vector3();

        int rndX = (int)model.gridPosition.x;// UnityEngine.Random.Range(0, (int)grid.x);
        int rndY = (int)model.gridPosition.y;//UnityEngine.Random.Range(0, (int)grid.y);
        
       
        float tempX = (ActionArea.rect.max.x - ActionArea.rect.min.x) / WavesList[CurrentWave].grid.x;
        float tempY = (ActionArea.rect.max.y - ActionArea.rect.min.y) / WavesList[CurrentWave].grid.y;

        tempV3.x = rndX * tempX + ActionArea.rect.min.x;
        tempV3.y = rndY * tempY + ActionArea.rect.min.y;

        GameObject tempObject = (GameObject)Instantiate(ActionButtonPrefab, tempV3, Quaternion.identity);
        tempObject.transform.SetParent(ActionArea.transform, false);
        ActionButtonView actionView = tempObject.GetComponent<ActionButtonView>();
        actionView.chainController = ChainController;

        actionView.Build(model);
        actionView.id = _actionList.Count;
        actionView.gameObject.SetActive(true);
        _actionList.Add(actionView);

        actionView.finishCallback = (() =>
        {
            _actionList.Remove(actionView);
            ChainController.FinishedActions++;
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
                        gaugeAccum = GaugeValues.Miss;
                        break;
                    case FeedbackStateType.GOOD:
                        gaugeAccum = GaugeValues.Good;
                        actionPoints = 1;
                        break;
                    case FeedbackStateType.GREAT:
                        gaugeAccum = GaugeValues.Great;
                        actionPoints = 2;
                        break;
                    case FeedbackStateType.BAD:
                        gaugeAccum = GaugeValues.Miss;
                        break;
                    case FeedbackStateType.PERFECT:
                        gaugeAccum = GaugeValues.Perfect;
                        actionPoints = 3;
                        break;
                    case FeedbackStateType.TOLATE:
                        gaugeAccum = GaugeValues.ToLate;
                        break;
                    case FeedbackStateType.TOEARLY:
                        gaugeAccum = GaugeValues.ToEarly;
                        break;
                    default:
                        break;
                }
            }

            ChainActionType chainActionType = ChainController.UpdateChain(actionView.currentFeedbackState);

            ChainFinishedType finishedType = ChainFinishedType.BAD;

            //print(chainController.FinishedActions +"=="+ chainController.ActionsInWave);
            if (ChainController.FinishedActions == ChainController.ActionsInWave)
            {

                float finishedFactor = ChainController.chainLevel / (ChainController.ActionsInWave * 3);
                
                if (finishedFactor >= 0.85)
                {
                    finishedType = ChainFinishedType.PERFECT;

                }
                else if (finishedFactor >= 0.5)
                {
                    finishedType = ChainFinishedType.GOOD;
                }
                else
                {
                    _beatCounter = -999;
                    PauseGame();
                }


                ChainController.FinishChain(finishedType);

                //print("FINISH");
                //if (chainActionType == ChainActionType.FINISHED)
                //{
                //    chainController.FinishChain(finishedType);
                //}
                //else if (chainActionType == ChainActionType.BROKE)
                //{
                //    //finishedType = ChainController.ChainFinishedType.BAD;
                //    chainController.BreakChain(finishedType);
                //    breakWave();
                //}

                updateGameState(finishedType);
                Zombie.SetAnimation(finishedType);
            }

            


            LevelGauge += gaugeAccum;

            //updateLevelGauge();

            if (actionPoints + goldenBrain > 0)
            {
                for (int i = 0; i < actionPoints + goldenBrain; i++)
                {
                    GameObject tempParticle;
                    tempParticle = (GameObject)Instantiate(ParticlePrefab, new Vector3(), Quaternion.identity);
                    tempParticle.transform.SetParent(ParticlesContainer.transform, false);
                    ActionParticleView particleView = tempParticle.GetComponent<ActionParticleView>();

                    float distance = Vector2.Distance(new Vector2(PointsLabelRect.position.x, PointsLabelRect.position.y), new Vector2(tempV3.x, tempV3.y));
                    
                    particleView.time = Screen.height / distance * 0.8f;
                    particleView.delay = 0.1f + i * 0.2f;
                    particleView.destiny = PointsLabelRect.position;
                    particleView.rectTarget = PointsLabelRect;

                    float newPosX = tempV3.x;
                    float newPosY = tempV3.y;
                    if (goldenBrain > 0)
                    {
                        particleView.destiny = CurrencyRect.position;
                        particleView.rectTarget = CurrencyRect;
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
                    particleView.Build(goldenBrain > 0, actionPoints + goldenBrain, i);                    
                }                
            }
        });
        model.placed = true;
    }

    private void updateGameState(ChainFinishedType chainActionType)
    {
        switch (chainActionType)
        {
            case ChainFinishedType.PERFECT:
                break;
            case ChainFinishedType.GOOD:
                break;
            case ChainFinishedType.BAD:
                reduceLife();
                break;
            default:
                break;
        }
    }



    private void updateCurrency(int currencyPoints)
    {
        currency += currencyPoints;
        updateCurrencyLabel();
    }
    private void updatePoints(int actionPoints)
    {
        Points += actionPoints;
        updatePointsLabel();

        if(LevelGauge > MaxGauge)
        {
            LevelGauge = MaxGauge/2;
            AudioController.UpgradeAudioController();
            CurrentLevel++;
            
        }
    }
    private void updatePointsLabel()
    {
        PointsLabel.text = Points.ToString();
    }
    private void updateCurrencyLabel()
    {
        CurrencyLabel.text = currency.ToString();
    }
}
