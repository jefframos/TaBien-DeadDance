using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameEffects : MonoBehaviour {

	public void ShakePos(RectTransform rect)
    {
        Sequence positionSequence = DOTween.Sequence();
        float time = 0.8f;
        int steps = 8;
        float force = 80f;
        Vector3 currentPosition = rect.localPosition;
        Vector3 targetPosition;
        for (int i = 0; i < steps; i++)
        {
                targetPosition = new Vector3(UnityEngine.Random.Range(currentPosition.x - force / 2, currentPosition.x + force / 2),
                UnityEngine.Random.Range(currentPosition.y - force / 2, currentPosition.y + force / 2),
                0);
                positionSequence.Append(rect.DOLocalMove(targetPosition, time / steps));
            print(time / steps);        
        }        
        positionSequence.Append(rect.DOLocalMove(currentPosition, time / steps));
    }

    public void ShakeScale(RectTransform rect)
    {
        Sequence positionSequence = DOTween.Sequence();
        float time = 1f;
        int steps = 8;
        float force = 0.2f;
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
