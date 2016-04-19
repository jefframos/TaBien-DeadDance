using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MadnessFactor : MonoBehaviour {

    public static float Multiplier;
    public static bool MadnessMode;
    private static Tweener multiplierTween;

    public static void NoMoreMadness(float time = 1)
    {
        MadnessMode = false;
        multiplierTween.Kill();
        multiplierTween = DOTween.To(() => Multiplier, x => Multiplier = x, 1, time);
        //Multiplier = 1;
    }
    public static void Madness(float factor, float time = 3)
    {
        MadnessMode = true;
        multiplierTween.Kill();
        multiplierTween = DOTween.To(() => Multiplier, x => Multiplier = x, factor, time);
    }
}
