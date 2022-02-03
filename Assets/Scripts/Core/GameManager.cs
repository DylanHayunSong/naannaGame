using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int currentOrder = 0;
    public int currentRound = 0;
    public bool isCardClickable = true;

    public List<int> cardNumList = new List<int>();

    public Action<int> cardClickedAction;
    public Action RoundStartAction;

    protected override void Init ()
    {
        base.Init();
    }

    private void Start ()
    {
        if(RoundStartAction != null)
        {
            RoundStartAction.Invoke();
        }
        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void SingletonClassLoad()
    {
        if(GameManager.inst == null)
        {
            GameObject gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>();
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

        if(RoundStartAction != null)
        {
            RoundStartAction.Invoke();
        }
    }
}
