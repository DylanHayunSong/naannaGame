using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePage : MonoBehaviour
{
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private GameObject gameOverArea;
    [SerializeField]
    private Button saveBtn;
    [SerializeField]
    private Button tryAgainBtn;
    [SerializeField]
    private Text timerText;

    private void Start ()
    {
        GameManager.inst.pageChangeAction += OnPageChanged;
        GameManager.inst.RoundStartAction += () => { roundText.text = (GameManager.inst.currentRound + 1).ToString(); };
        GameManager.inst.RoundStartAction += OnGameStarted;
        GameManager.inst.GameOverAction += OverGame;

        tryAgainBtn.onClick.AddListener(OnClickTryAgain);
        saveBtn.onClick.AddListener(OnClickSave);

        gameObject.SetActive(false);
        gameOverArea.SetActive(false);
    }

    private void OnPageChanged (GameManager.Pages page)
    {
        switch (page)
        {
            case GameManager.Pages.PAGE_TITLE:
                gameObject.SetActive(false);
                gameOverArea.SetActive(false);
                break;
            case GameManager.Pages.PAGE_INGAME:
                gameObject.SetActive(true);
                GameManager.inst.RoundStart();
                TimerStart();
                break;
        }
    }

    private void OnGameStarted ()
    {
        gameOverArea.SetActive(false);
    }

    private void OverGame ()
    {
        gameOverArea.SetActive(true);
        GameManager.inst.StopTimer();
        //GameManager.inst.ChangePage(GameManager.Pages.PAGE_TITLE);

        //gameOverArea.SetActive(true);
    }

    private void OnClickSave ()
    {
        GameCaptureManager.inst.GetCaptureScreen((value) => { JavascriptNetworkManager.ModalOn(); });

        //JavascriptNetworkManager.ModalOn();
    }

    private void OnClickTryAgain ()
    {
        TimerStart();
        GameManager.inst.RoundStart();
    }

    private void TimerStart()
    {
        GameManager.inst.StartTimer((value) => { timerText.text = DeltatimeToTime(value); });
    }

    private string DeltatimeToTime(float time)
    {
        int minute =  Mathf.FloorToInt(time/60);
        int second = Mathf.FloorToInt(time-(minute*60));
        int floatSecond = Mathf.FloorToInt((time - minute * 60 - second) * 100);

        return string.Format("{0:D2} : {1:D2} : {2:D2}", minute, second, floatSecond);

    }
}
