using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ZombiePartsController{

    public string head;
    public string basePath = "Game/Chars/";
    public string path = "Zombie2";
    public List<string> names;
    public bool working;
    public int pathID;
    private List<SpriteRenderer> renderers;
    public PartsModel CurrentModel;
    public ZombiePartsController(SpriteRenderer[] rendererList, List<string> partsNames)
    {
        names = partsNames;

        renderers = new List<SpriteRenderer>();

        foreach (var renderer in rendererList)
        {
            if (renderer.sprite != null)
            {
                string spriteName = renderer.sprite.name;
                if (names.Contains(spriteName))
                {
                    renderers.Add(renderer);
                }
            }
        }

    }

    internal void ForceFadeIn(float _time, float _delay = 0, Action callback = null)
    {
        foreach (var renderer in renderers)
        {
            renderer.DOKill();
            renderer.color = new Color(1, 1, 1, 0f);
            renderer.DOFade(1f, _time).OnComplete(() =>
            {
                if (callback != null)
                {
                    callback();
                }
            }).SetDelay(_delay);
        }
    }

    internal void ForceFadeOut(float _time, float _delay = 0, Action callback = null)
    {
        foreach (var renderer in renderers)
        {
            renderer.color = new Color(1, 1, 1, 1f);
            renderer.DOKill();
            renderer.DOFade(0, _time).OnComplete(() => {
                if (callback != null)
                {
                    callback();
                }
            }).SetDelay(_delay);
        }
    }

    internal void FadeOut(float _time, float _delay = 0, Action callback = null)
    {
        foreach (var renderer in renderers)
        {
            renderer.DOFade(0, _time).OnComplete(()=> {
                if (callback != null)
                {
                    callback();
                }
            }).SetDelay(_delay);
        }
    }

    internal void FadeIn(float _time, float _delay = 0, Action callback = null)
    {
        foreach (var renderer in renderers)
        {
            renderer.DOFade(1, _time).OnComplete(() => {
                if (callback != null)
                {
                    callback();
                }
            }).SetDelay(_delay);
        }
    }
    internal void UpdateParts(PartsModel partsModel)
    {
        CurrentModel = partsModel;


        var subSprites = Resources.LoadAll<Sprite>(basePath + CurrentModel.Path);

        foreach (var renderer in renderers)
        {

            var newSprite = Array.Find(subSprites, item => item.name == renderer.name);
            if (newSprite)
            {
                renderer.sprite = newSprite;
                renderer.DOFade(0.5f, 0.3f);
            }
        }
    }
}
