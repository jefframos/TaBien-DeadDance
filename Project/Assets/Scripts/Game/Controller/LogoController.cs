using UnityEngine;
using System.Collections;
using System;

public class LogoController : MonoBehaviour {
    public RectTransform RectTurbo;
    public RectTransform RectHardcore;
    internal void UpdateLogoStatus()
    {
        switch (MadnessFactor.GameMode)
        {
            case GameModeType.STANDARD:
                RectTurbo.gameObject.SetActive(false);
                RectHardcore.gameObject.SetActive(false);
                break;
            case GameModeType.TURBO:
                RectTurbo.gameObject.SetActive(true);
                RectHardcore.gameObject.SetActive(false);
                break;
            case GameModeType.HARDCORE:
                RectTurbo.gameObject.SetActive(true);
                RectHardcore.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }
}
