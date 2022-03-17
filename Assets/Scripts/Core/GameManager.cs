using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int currentOrder = 0;
    public int currentRound = 0;
    public bool isCardClickable = true;
    public bool IsSoundOn { get { return isSoundOn; } set { isSoundOn = value; if (SoundOnAction != null) SoundOnAction.Invoke(value); } }
    private bool isSoundOn = true;

    public List<int> cardNumList = new List<int>();

    public Action<int> cardClickedAction;
    public Action RoundStartAction;
    public Action GameOverAction;
    public Action<Pages> pageChangeAction;

    // Actions for sound
    public Action<bool> SoundOnAction;
    public Action<SFXSounds> PlaySfxAction;

    // Actions for Set User Datas
    public Action<string> SetGameStartTimeAction;
    public Action<string> SetGameEndTimeAction;
    public Action<int> SetLastLevelAction;
    public Action<Texture> SetResultCaptureAction;

    private Coroutine timerCoroutine = null;

    public enum Pages { PAGE_TITLE, PAGE_INGAME}
    private Pages currentPage;

    protected override void Init ()
    {
        base.Init();
    }

    private void Start ()
    {
        
    }

    private void Update ()
    {
        
    }

    public void PlaySfxSound(SFXSounds sound)
    {
        if(PlaySfxAction != null)
        {
            PlaySfxAction.Invoke(sound);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void SingletonClassLoad()
    {
        if(GameManager.inst == null)
        {
            GameObject gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>();
        }
        if(GameSoundManager.inst == null)
        {
            GameObject gameSoundManager = new GameObject("GameSoundManager");
            gameSoundManager.AddComponent<GameSoundManager>();
        }
        if(ResourceCacheManager.inst == null)
        {
            GameObject resourceCacheManagerPrefab = Resources.Load("Prefabs/ResourceCacheManager") as GameObject;
            GameObject resourceCacheManager = Instantiate(resourceCacheManagerPrefab);
        }
        if(UserDataManager.inst == null)
        {
            GameObject userDataManager = new GameObject("UserDataManager");
            userDataManager.AddComponent<UserDataManager>();
        }
        if(GameCaptureManager.inst == null)
        {
            GameObject gameCaptureManager = new GameObject("GameCaptureManager");
            gameCaptureManager.AddComponent<GameCaptureManager>();
        }
    }

    public void OnCardClicked(int cardNum)
    {
        if(cardClickedAction != null)
        {
            cardClickedAction.Invoke(cardNum);
        }
    }

    public void ResetGame()
    {
        currentOrder = 0;
        currentRound = 0;
        isCardClickable = false;
        cardNumList = new List<int>();

        //if(RoundStartAction != null)
        //{
        //    RoundStartAction.Invoke();
        //}
    }

    public void ChangePage(Pages nextPage)
    {
        currentPage = nextPage;
        if(pageChangeAction != null)
        {
            pageChangeAction.Invoke(nextPage);
        }
    }

    public void RoundStart()
    {
        SetGameStartTime();

        if (RoundStartAction != null)
        {
            RoundStartAction.Invoke();
        }
    }

    public void GameOver()
    {
        SetLastLevel();
        SetGameEndTime();

        if (GameOverAction != null)
        {
            GameOverAction.Invoke();
        }
        
        ResetGame();
    }

    public void SetGameStartTime()
    {
        if(SetGameStartTimeAction != null)
        {
            string timeString = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            SetGameStartTimeAction.Invoke(timeString);
        }
    }

    public void SetGameEndTime()
    {
        if(SetGameEndTimeAction != null)
        {
            string timestring = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            SetGameEndTimeAction.Invoke(timestring);
        }
    }

    public void SetLastLevel()
    {
        if(SetLastLevelAction != null)
        {
            SetLastLevelAction.Invoke(currentRound + 1);
        }
    }

    public void StartTimer(Action<float> timerAction)
    {
        timerCoroutine = StartCoroutine(TimerRoutine(timerAction));
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;
    }

    private IEnumerator TimerRoutine(Action<float> timeAction)
    {
        float time = 0;

        while(true)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;

            if(timeAction != null)
            {
                timeAction.Invoke(time);
            }
        }
        
    }
}
