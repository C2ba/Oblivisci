using System;
using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField] float fadeDelay = 0.07f;
    Material mat;

    bool isFadingOut = false;
    Coroutine fadingCoroutine;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public IEnumerator Fade(bool fadeOut)
    {
        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
            fadingCoroutine = null;
        }

        isFadingOut = fadeOut;
        yield return fadingCoroutine = StartCoroutine(PlayFade(fadeOut));
    }

    IEnumerator PlayFade(bool fadeOut)
    {
        float alpha = mat.GetFloat("_Alpha");

        float desiredAlpha = fadeOut ? 0f : 1f;
        float direction = fadeOut ? -1f : 1f;

        yield return null;

        while (!Mathf.Approximately(alpha, desiredAlpha))
        {
            alpha += direction * 0.05f;
            alpha = Mathf.Clamp01(alpha);

            mat.SetFloat("_Alpha", alpha);

            yield return new WaitForSeconds(fadeDelay);
        }

        mat.SetFloat("_Alpha", desiredAlpha);
    }

    public void SetAlpha(float num)
    {
        mat.SetFloat("_Alpha", num);
    }
}
