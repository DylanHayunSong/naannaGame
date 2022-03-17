using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCaptureManager : SingletonBehaviour<GameCaptureManager>
{
    [SerializeField]
    private Texture resultCaptureTex;

    public void GetCaptureScreen(Action<Texture> action)
    {
        StartCoroutine(CaptureScreen(action));
    }

    private IEnumerator CaptureScreen(Action<Texture> action)
    {
        yield return new WaitForEndOfFrame();

        resultCaptureTex = ScreenCapture.CaptureScreenshotAsTexture();

        action.Invoke(resultCaptureTex);
    }
}
