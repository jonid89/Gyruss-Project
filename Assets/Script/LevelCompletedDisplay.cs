using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedDisplay : MonoBehaviour
{
    public float fadeOutDuration = 2f;

    private CanvasGroup popupCanvasGroup;

    private void OnEnable()
    {
        popupCanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        //Resetting Alpha values of LevelCompleted Canvas 
        float timer = 0f;
        float startAlpha = 1f;
        float targetAlpha = 0f;
        popupCanvasGroup.alpha = startAlpha;

        //Lerping Canvas Alpha value for fading effect
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeOutDuration);
            popupCanvasGroup.alpha = alpha;
            yield return null;
        }

        popupCanvasGroup.alpha = targetAlpha;

        gameObject.SetActive(false);
    }
}