﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadParts{

    public string head;
    public string basePath = "Game/Chars/";
    public string path = "Zombie2";
    public List<string> names;
    public bool working;
    public int pathID;
    public HeadParts()
    {
        names = new List<string>(new string[] {
            "eye1_closed",
            "eye2_closed",
            "ear_left",
            "ear_right",
            "hair",
            "eye1",
            "eye2",
            "mouth1",
            "pupila1",
            "pupila2",
            "head",
            "queixo"
        });
    }

    internal void UpdateParts(SpriteRenderer[] rendererList, string _path)
    {
        path = _path;

        
        var subSprites = Resources.LoadAll<Sprite>(basePath + path + "/head");
        foreach (var renderer in rendererList)
        {
            string spriteName = renderer.sprite.name;
            
            if (names.Contains(spriteName))
            {
                var newSprite = Array.Find(subSprites, item => item.name == spriteName);
                Debug.Log(newSprite);
                //TODO fix this
                if (newSprite)
                    renderer.sprite = newSprite;
                
            }
        }
    }
}
