using UnityEngine;
using System.Collections;

public class PlayButtonController : MonoBehaviour {
    public RectTransform RectTransformStandard;
    public RectTransform RectTransformTurbo;
    public RectTransform RectTransformHardcore;
    // Use this for initialization
    void Start () {
        UpdateShape();
    }
	
	// Update is called once per frame
	public void UpdateShape () {
        RectTransformStandard.gameObject.SetActive(false);
        RectTransformTurbo.gameObject.SetActive(false);
        RectTransformHardcore.gameObject.SetActive(false);
        switch (MadnessFactor.GameMode)
        {
            case GameModeType.STANDARD:
                RectTransformStandard.gameObject.SetActive(true);
                break;
            case GameModeType.TURBO:
                RectTransformTurbo.gameObject.SetActive(true);
                break;
            case GameModeType.HARDCORE:
                RectTransformHardcore.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
