using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private Card[] cards = null;
    [SerializeField]
    private Coroutine roundStartedCoroutine = null;

    private void Awake ()
    {
        cards = GetComponentsInChildren<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].cardNum = i;
        }
    }

    private void Start ()
    {
        GameManager.inst.cardClickedAction += CardCorrectCheck;
        GameManager.inst.RoundStartAction += StartRound;
    }

    public void StartRound ()
    {
        if (roundStartedCoroutine == null)
        {
            roundStartedCoroutine = StartCoroutine(RoundStartRoutine());
        }
    }

    private IEnumerator RoundStartRoutine ()
    {
        GameManager.inst.isCardClickable = false;

        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].btn.interactable = false;
        }

        for (int i = 0; i <= GameManager.inst.currentRound; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            if (i == GameManager.inst.currentRound)
            {
                int randomNum = Random.Range(0, cards.Length);
                GameManager.inst.cardNumList.Add(randomNum);

                cards[randomNum].PlayCardBlinkAnim();
            }
            else
            {
                cards[GameManager.inst.cardNumList[i]].PlayCardBlinkAnim();
            }
        }
        roundStartedCoroutine = null;

        GameManager.inst.isCardClickable = true;

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].btn.interactable = true;
        }
    }

    private void CardCorrectCheck (int cardNum)
    {
        if (cardNum == GameManager.inst.cardNumList[GameManager.inst.currentOrder])
        {
            if (GameManager.inst.currentRound == GameManager.inst.currentOrder)
            {
                GameManager.inst.currentRound++;
                GameManager.inst.currentOrder = 0;

                GameManager.inst.isCardClickable = false;

                GameManager.inst.RoundStartAction.Invoke();
            }
            else
            {
                GameManager.inst.currentOrder++;
            }
        }
        else
        {
            GameManager.inst.ResetGame();
        }
    }
}
