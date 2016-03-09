using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public RectTransform actionArea;
    public GameObject actionButtonPrefab;
    public float gameTime = 0;
    private List<ActionButtonView> buttonWave;
    public List<WaveModel> wavesList;
    public int currentWave = 1;
    public int bpm = 126;
    private float _beatCounter = 0;
    public AudioSource mainAudioSource;
    public Metronome metronome;

    public Text beatCounter;
    // Use this for initialization
    void Start () {
        foreach (ActionButtonModel item in wavesList[0].actionList)
        {
            //print(item.gridPosition);
        }

        mainAudioSource.Play();
        metronome.beatCallback = BeatCallback;
    }

    private void BeatCallback()
    {
        _beatCounter += 0.25f;

        beatCounter.text = Mathf.Floor(_beatCounter).ToString();
    }

    // Update is called once per frame
    void Update () {

        //print(Time.fixedDeltaTime);
        if (!mainAudioSource.isPlaying)
        {
            return;
        }
        //gameTime += Time.deltaTime * timeStretch;

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
        tempObject.GetComponent<ActionButtonView>().Build(model);
        model.placed = true;
    }
}
