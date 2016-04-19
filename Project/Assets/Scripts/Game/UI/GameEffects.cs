using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameEffects : MonoBehaviour {

    public GameObject MadnessLayer;
    public GameObject BackgroundLayer;
    void Awake()
    {
        StopMadness();
    }
	public void StartMadness()
    {
        MadnessLayer.SetActive(true);
    }
    public void StopMadness()
    {
        MadnessLayer.SetActive(false);
    }
    public void ShakePos(RectTransform rect, float _force = 50f, float _time = 0.8f, int _steps = 8)
    {
        Sequence positionSequence = DOTween.Sequence();
        float time = _time;
        int steps = _steps;
        float force = _force;
        Vector3 currentPosition = rect.localPosition;
        Vector3 targetPosition;
        for (int i = 0; i < steps; i++)
        {
                targetPosition = new Vector3(UnityEngine.Random.Range(currentPosition.x - force / 2, currentPosition.x + force / 2),
                UnityEngine.Random.Range(currentPosition.y - force / 2, currentPosition.y + force / 2),
                0);
                positionSequence.Append(rect.DOLocalMove(targetPosition, time / steps));
            //print(time / steps);        
        }        
        positionSequence.Append(rect.DOLocalMove(currentPosition, time / steps));
    }

    public void ShakeScale(RectTransform rect, float _force = 0.2f, float _time = 0.8f, int _steps = 8)
    {
        Sequence positionSequence = DOTween.Sequence();
        float time = _time;
        int steps = _steps;
        float force = _force;
        float currentScale = rect.localScale.x;
        float targetScale;
        for (int i = 0; i < steps; i++)
        {
            targetScale = UnityEngine.Random.Range(0, force) - force / 2;
            positionSequence.Append(rect.DOScale(currentScale + targetScale, time / steps));
        }
        positionSequence.Append(rect.DOScale(currentScale, time / steps));
    }
}
