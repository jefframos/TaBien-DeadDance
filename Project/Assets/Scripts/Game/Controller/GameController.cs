using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public RectTransform actionArea;
    public GameObject actionButtonPrefab;
    public float gameTime = 0;
    private List<ActionButtonView> buttonWave;
    public float timeStretch = 1;
    public List<WaveModel> wavesList;
    public int currentWave = 1;
    // Use this for initialization
    void Start () {
        foreach (ActionButtonModel item in wavesList[0].actionList)
        {
            print(item.gridPosition);
        }
    }
	
	// Update is called once per frame
	void Update () {

        gameTime += Time.deltaTime * timeStretch;

        foreach(ActionButtonModel model in wavesList[currentWave].actionList)
        {
            if(!model.placed && gameTime > ((model.timeToShow * timeStretch) - (model.timeToTap * timeStretch)))
            {
                addAction(model);
            }
        }

        if (Mathf.Floor(gameTime) > wavesList[currentWave].duration * timeStretch)
        {
            gameTime = 0;
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
        tempObject.GetComponent<ActionButtonView>().Build(model, timeStretch);
        model.placed = true;
    }
}
