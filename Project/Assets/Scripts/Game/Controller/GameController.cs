using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {
    public RectTransform actionArea;
    public GameObject actionButtonPrefab;
    public float gameTime = 0;
    private List<ActionButtonView> buttonWave;
    public int testWave = -1;

    public List<WaveModel> wavesList;
    public List<BeatterView> beatterList;
    public int currentWave = 1;
    private float _beatCounter = 0;
    private int _beatAcum = 0;
    public AudioSource mainAudioSource;
    public Metronome metronome;

    public Animator zombieAnimator;

    public Text beatCounter;

    
    public ZombieView zombieView;
    // Use this for initialization
    void Start () {
        
        getWave();

        mainAudioSource.Play();
        metronome.beatCallback = BeatCallback;
        
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
        
        if (!mainAudioSource.isPlaying)
        {
            return;
        }
        foreach(ActionButtonModel model in wavesList[currentWave].actionList)
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
        actionView.callback = (() =>
        {
            if(actionView.currentFeedbackState != ActionButtonView.FeedbackStateType.MISS)
                zombieView.updatePart(tempV3);
        });
        model.placed = true;
    }
}
