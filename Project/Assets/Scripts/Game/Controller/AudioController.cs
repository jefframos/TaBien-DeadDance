using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
public class AudioController : MonoBehaviour {
    [Serializable]
    public class AudioLoopData
    {
        public AudioClip audioClip;
        public int BPM = 126;
    }
    public AudioSource audioSource;
    public AudioSource audioSourceAux;
    public AudioSource ambientSource;
    public List<AudioLoopData> audioLoopDataList;
    public List<AudioClip> ambientAudioClipList;
    public AudioLoopData currentAudioLoop;
    public float maxAmbientVolume = 0.3f;
    // Use this for initialization
    private int currentDataID = 0;
    void Start()
    {

        ambientSource.gameObject.SetActive(true);
        ambientSource.clip = ambientAudioClipList[UnityEngine.Random.Range(0, ambientAudioClipList.Count)];
        ambientSource.Play();
        ambientSource.volume = 0;
        ambientSource.DOFade(maxAmbientVolume, 2f);
    }
    public void InitAudioController () {
        currentDataID = 0;
        currentAudioLoop = audioLoopDataList[currentDataID];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();

        
        ambientSource.DOFade(0, 1f).OnComplete(()=>
        {
            ambientSource.Stop();
        });
        audioSource.volume = 0;
        audioSource.DOFade(maxAmbientVolume, 2f);
    }

    // Update is called once per frame
    public void UpgradeAudioController() {
        if (currentDataID < audioLoopDataList.Count - 1)
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

        currentAudioLoop = audioLoopDataList[currentDataID];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();
        audioSource.volume = 0;
        audioSource.DOFade(maxAmbientVolume, 3f);
    }

    internal void IncreaseBPM()
    {
        if(currentAudioLoop == audioLoopDataList[1])
        {
            return;
        }
        currentAudioLoop = audioLoopDataList[1];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.volume = 0.1f;
        audioSource.Play();
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
