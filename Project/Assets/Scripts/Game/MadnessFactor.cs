using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class MadnessFactor : MonoBehaviour {

    public static float Multiplier;
    public static float GameSpeed = 1;
    public static bool MadnessMode;
    public static GameModeType GameMode;
    private static Tweener multiplierTween;

    public static void NoMoreMadness(float time = 1)
    {
        MadnessMode = false;
        multiplierTween.Kill();
        multiplierTween = DOTween.To(() => Multiplier, x => Multiplier = x, GameSpeed, time);
        //Multiplier = 1;
    }
    public static void Madness(float factor, float time = 3)
    {
        MadnessMode = true;
        multiplierTween.Kill();
        multiplierTween = DOTween.To(() => Multiplier, x => Multiplier = x, (factor + GameSpeed), time);
    }

    internal static void SetStandardMode()
    {
        GameMode = GameModeType.STANDARD;
        GameSpeed = 1;
        Multiplier = 1;
    }

    internal static void SetTurboMode(float speed)
    {
        GameMode = GameModeType.TURBO;
        GameSpeed = speed;
        Multiplier = speed;
    }

    internal static void SetHardcoreMode(float speed)
    {
        GameMode = GameModeType.HARDCORE;
        GameSpeed = speed;
        Multiplier = speed;
    }
    internal static int GetBrains(int perfectInARow)
    {
        int acc = 0;
        switch (GameMode)
        {
            case GameModeType.STANDARD:
                break;
            case GameModeType.TURBO:
                acc = 1;
                break;
            case GameModeType.HARDCORE:
                acc = 2;
                break;
            default:
                break;
        }
        return perfectInARow + acc;
    }
    internal static int GetPoints(int points)
    {
        int acc = 0;
        switch (GameMode)
        {
            case GameModeType.STANDARD:
                break;
            case GameModeType.TURBO:
                acc = 1;
                break;
            case GameModeType.HARDCORE:
                acc = 2;
                break;
            default:
                break;
        }
        return points + acc;
    }
}
