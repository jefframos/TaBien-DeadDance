using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
    [Serializable]
    public class AudioLoopData
    {
        public AudioClip audioClip;
        public int BPM = 126;
    }
    public AudioSource audioSource;
    public List<AudioLoopData> audioLoopDataList;
    public AudioLoopData currentAudioLoop;
        // Use this for initialization
    public void InitAudioController () {
        currentAudioLoop = audioLoopDataList[0];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();
    }

    // Update is called once per frame
    public void UpdateAudioController() {
	
	}

    internal void IncreaseBPM()
    {
        if(currentAudioLoop == audioLoopDataList[1])
        {
            return;
        }
        currentAudioLoop = audioLoopDataList[1];
        audioSource.clip = currentAudioLoop.audioClip;
        audioSource.Play();
    }

    internal void Reset()
    {
        audioSource.Stop();
    }
}
