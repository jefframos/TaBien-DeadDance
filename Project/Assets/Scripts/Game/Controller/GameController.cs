using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public RectTransform actionArea;
    public GameObject actionButtonPrefab;
    public float gameTime = 0;
    private List<ActionButtonView> buttonWave;
    [SerializeField]
    public static Vector2 grid = new Vector2(6, 8);

    public List<WaveModel> wavesList;
    public int currentWave = 0;
    // Use this for initialization
    void Start () {

        }
	
	// Update is called once per frame
	void Update () {

        gameTime += Time.deltaTime;

        foreach(ActionButtonModel model in wavesList[currentWave].actionList)
        {
            if(!model.placed && gameTime > model.timeToShow)
            {
                addAction(model);
            }
        }

        if (Mathf.Floor(gameTime) > 10)
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

        float tempX = (actionArea.rect.max.x - actionArea.rect.min.x) / grid.x;
        float tempY = (actionArea.rect.max.y - actionArea.rect.min.y) / grid.y;

        tempV3.x = rndX * tempX + actionArea.rect.min.x;
        tempV3.y = rndY * tempY + actionArea.rect.min.y;

        GameObject tempObject = (GameObject)Instantiate(actionButtonPrefab, tempV3, Quaternion.identity);
        tempObject.transform.SetParent(actionArea.transform, false);
        tempObject.GetComponent<ActionButtonView>().Build(model);
        model.placed = true;
    }
}
