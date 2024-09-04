using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCanvasGroup : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] float waitTime = 20;
    [SerializeField] float FadeTime = 2;

    private void Awake()
    {
        waitTime = Application.isEditor ? 3 : waitTime;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitTime);

        float t = 0;
        while(t < FadeTime)
        {
            t += Time.deltaTime;

            cg.alpha = Mathf.Lerp(1, 0, t / FadeTime);

            yield return null;
        }        
    }
}
