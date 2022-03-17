using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private Card[] cards = null;
    [SerializeField]
    private Coroutine roundStartedCoroutine = null;
    [SerializeField]
    private float duration = 1;

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

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].btn.interactable = false;
        }

        duration *= 0.95f;

        for (int i = 0; i <= GameManager.inst.currentRound; i++)
        {
            yield return new WaitForSecondsRealtime(duration);
            //GameSoundManager.inst.SetSfxPitch(1 + i * 0.05f);
            
            if (i == GameManager.inst.currentRound)
            {
                
                int randomNum = Random.Range(0, cards.Length);
                GameManager.inst.cardNumList.Add(randomNum);

                cards[randomNum].PlayCardBlinkAnim(duration);
            }
            else
            {
                cards[GameManager.inst.cardNumList[i]].PlayCardBlinkAnim(duration);
            }
        }

        yield return new WaitForSecondsRealtime(1);

        roundStartedCoroutine = null;

        GameManager.inst.isCardClickable = true;

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].btn.interactable = true;
        }
        GameSoundManager.inst.ResetSfxPitch();
    }

    private void CardCorrectCheck (int cardNum)
    {
        if (cardNum == GameManager.inst.cardNumList[GameManager.inst.currentOrder])
        {
            GameSoundManager.inst.SetSfxPitch(0.85f + cardNum * 0.025f);
            if (GameManager.inst.currentRound == GameManager.inst.currentOrder)
            {
                //GameSoundManager.inst.SetSfxPitch(1 + GameManager.inst.currentOrder * 0.05f);
                GameManager.inst.PlaySfxSound(SFXSounds.SFX_NA);
                GameManager.inst.currentRound++;
                GameManager.inst.currentOrder = 0;

                GameManager.inst.isCardClickable = false;

                GameManager.inst.RoundStartAction.Invoke();
            }
            else
            {
                //GameSoundManager.inst.SetSfxPitch(1 + GameManager.inst.currentOrder * 0.05f);
                GameManager.inst.PlaySfxSound(SFXSounds.SFX_NA);
                GameManager.inst.currentOrder++;
            }
        }
        else
        {
            duration = 1;
            GameManager.inst.GameOver();
            GameSoundManager.inst.ResetSfxPitch();
            GameManager.inst.PlaySfxSound(SFXSounds.SFX_ANNA);
        }
    }
}
