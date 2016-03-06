using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public RectTransform actionArea;
    public GameObject actionButtonPrefab;
    public float gameTime = 0;
    private List<ActionButtonView> buttonWave;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameTime += Time.deltaTime;

        if(Mathf.Floor(gameTime) % 3 == 1)
        {
            addAction();
            gameTime = 0;
            //print(gameTime);
        }

    }

    private void addAction()
    {
        Vector3 tempV3 = new Vector3(UnityEngine.Random.Range(actionArea.rect.min.x, actionArea.rect.max.x),
                        UnityEngine.Random.Range(actionArea.rect.min.y, actionArea.rect.max.y),
                        0);
        GameObject tempObject = (GameObject)Instantiate(actionButtonPrefab, tempV3, Quaternion.identity);
        tempObject.transform.SetParent(actionArea.transform, false);
    }
}
