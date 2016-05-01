using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class EnvironmentModel : MonoBehaviour{
    #region COLORS
    public Color ColorActionToEarly = new Color((float)255 / 255, (float)44 / 255, (float)44 / 255);
    public Color ColorActionToLate = new Color((float)255 / 255, (float)44 / 255, (float)44 / 255);
    public Color ColorActionMiss = new Color((float)255 / 255, (float)44 / 255, (float)44 / 255);
    public Color ColorActionGreat = new Color((float)2 / 255, (float)255 / 255, (float)244 / 255);
    public Color ColorActionGood = new Color((float)99 / 255, (float)255 / 255, (float)113 / 255);
    public Color ColorActionPerfect = new Color((float)255 / 255, (float)69 / 255, (float)242 / 255);
    public Color ColorSpecial = new Color((float)255 / 255, (float)254 / 255, (float)1 / 255);
    public Color ColorMissSpecial = new Color((float)255 / 255, (float)254 / 255, (float)1 / 255);

    internal AudioClip GetRandomAmbient()
    {
        return AmbientAudioClipList[UnityEngine.Random.Range(0, AmbientAudioClipList.Count)];
    }

    public Color ColorStandardScale = new Color((float)28 / 255, (float)244 / 255, (float)238 / 255);
    public Color ColorStandardPosition = new Color((float)236 / 255, (float)19 / 255, (float)146 / 255);
    #endregion

    #region IMAGES
    public Sprite Background;
    #endregion

    #region AMBIENT SOUND
    [Serializable]
    public class AudioLoopData
    {
        public AudioClip audioClip;
        public int BPM = 126;
    }
    public List<AudioLoopData> AudioLoopDataList;
    public List<AudioClip> AmbientAudioClipList;
    #endregion

    #region COUNTDOWN
    public AudioClip CountdownAlert;
    public AudioClip CountdownEnd;
    #endregion

    #region CHAIN
    public AudioClip SoundPerfectWave;
    public AudioClip SoundGoodWave;
    public AudioClip SoundBadWave;
    #endregion

    #region ACTIONS
    public AudioClip SoundActionPerfect;
    public AudioClip SoundActionGreat;
    public AudioClip SoundActionGood;
    public AudioClip SoundActionWrong;
    public AudioClip SoundActionSpecial;
    public AudioClip SoundActionEntrance;
    #endregion
}