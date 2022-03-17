using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuPage : MonoBehaviour
{
    [SerializeField]
    private Button naLevelBtn;
    [SerializeField]
    private Button playBtn;

    private void Start ()
    {
        GameManager.inst.pageChangeAction += OnPageChanged;

        playBtn.onClick.AddListener(PlayGame);
        naLevelBtn.onClick.AddListener(ShowGraph);
    }

    private void OnPageChanged(GameManager.Pages page)
    {
        switch (page)
        {
            case GameManager.Pages.PAGE_TITLE:
                gameObject.SetActive(true);
                break;
            case GameManager.Pages.PAGE_INGAME:
                gameObject.SetActive(false);
                break;
        }
    }

    private void PlayGame ()
    {
        GameManager.inst.ChangePage(GameManager.Pages.PAGE_INGAME);
    }

    private void ShowGraph ()
    {
        JavascriptNetworkManager.ModalOn();
    }
}
