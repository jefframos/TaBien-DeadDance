using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
public class AudioController : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource audioSourceAux;
    public AudioSource ambientSource;

    public EnvironmentModel.AudioLoopData currentAudioLoop;
    public float maxAmbientVolume = 0.3f;
    // Use this for initialization
    private int currentDataID = 0;
    private bool started;
    void Start()
    {
        started = false;
        ambientSource.gameObject.SetActive(true);
        
        ambientSource.clip = GameDataManager.CurrentEnvironmentModel.GetRandomAmbient();
        ambientSource.Play();
        ambientSource.volume = 0;
        ambientSource.DOFade(maxAmbientVolume, 2f);
        
    }
    public void InitAudioController () {
        currentDataID = 0;
        currentAudioLoop = GameDataManager.CurrentEnvironmentModel.AudioLoopDataList[currentDataID];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();

        
        ambientSource.DOFade(0, 1f).OnComplete(()=>
        {
            ambientSource.Stop();
        });
        audioSource.volume = 0;
        audioSource.DOFade(maxAmbientVolume, 2f);
    }
    
    public void UpgradeAudioController() {
        if (currentDataID < GameDataManager.CurrentEnvironmentModel.AudioLoopDataList.Count - 1)
        {
            currentDataID++;
        }
        audioSourceAux.clip = currentAudioLoop.audioClip;
        audioSourceAux.time = audioSource.time;
        audioSourceAux.Play();
        audioSourceAux.DOFade(0, 2.5f).OnComplete(() =>
        {
            audioSourceAux.Stop();
        });

        currentAudioLoop = GameDataManager.CurrentEnvironmentModel.AudioLoopDataList[currentDataID];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();
        audioSource.volume = 0;
        audioSource.DOFade(maxAmbientVolume, 3f);
    }

    internal void IncreaseBPM()
    {
        if(currentAudioLoop == GameDataManager.CurrentEnvironmentModel.AudioLoopDataList[1])
        {
            return;
        }
        currentAudioLoop = GameDataManager.CurrentEnvironmentModel.AudioLoopDataList[1];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    internal void UpdateSongSpeed(float value)
    {
        if (started)
        {
            return;
        }
        started = true;
        audioSource.DOPitch(value, 2f).SetEase(Ease.InElastic);
        audioSourceAux.DOPitch(value, 2f).SetEase(Ease.InElastic);
    }

    internal void StopMadness()
    {
        started = false;
        audioSource.DOPitch(1f, 1f).SetEase(Ease.OutElastic);
        audioSourceAux.DOPitch(1f, 1f).SetEase(Ease.OutElastic);
    }


    internal void Reset()
    {
        audioSource.DOFade(0, 3f).OnComplete(() =>
        {
            audioSource.Stop();
        });
        ambientSource.Play();
        ambientSource.volume = 0;
        ambientSource.DOFade(maxAmbientVolume, 3f);
    }

    
}
