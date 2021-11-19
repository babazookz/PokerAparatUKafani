using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoScreen : MonoBehaviour
{
    public Canvas SplashScreenCanvas;
    private CanvasGroup splashAlpha;
    private Image logoImage;
    public List<Sprite> LogosToShow;

    void Awake()
    {
        if (SplashScreenCanvas != null)
        {
            logoImage = SplashScreenCanvas.GetComponentInChildren<Image>();
            splashAlpha = SplashScreenCanvas.GetComponent<CanvasGroup>();
            FadeIn();
        }
    }

    void Start()
    {
        //Invoke("FadeOutMethod", LoadTime);
    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation async = null;

        async = CustomSceneManager.LoadSceneAsync("game");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    void FadeIn()
    {
        if (LogosToShow != null && LogosToShow.Count > 0)
        {
            splashAlpha.alpha = 0f;
            Sprite _sprite = LogosToShow[0];
            if (_sprite != null)
            {
                LogosToShow.RemoveAt(0);
                logoImage.sprite = _sprite;
            }

            splashAlpha.DOFade(1f, 0.5f).SetDelay(.5f).OnComplete(() =>
            {
                FadeOut();
            }).Play();
        }
        else
        {
            StartCoroutine(LoadNewScene());
        }
    }

    void FadeOut()
    {
        splashAlpha.DOFade(0f, 0.5f).OnComplete(() =>
        {
            FadeIn();
        }).SetDelay(0.5f).Play();
    }
}
