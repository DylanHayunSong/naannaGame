using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public ButtonEX btn;
    [SerializeField]
    private Image btnImg;
    [SerializeField]
    private Image clickedImg;
    [SerializeField]
    private Image disabledImg;

    private Coroutine clickedAnimRoutine = null;

    public int cardNum;

    private void Awake ()
    {
        btn.onClick.AddListener(IsClicked);

        btn.buttonDownEvent.AddListener(OnButtonClickDown);
        btn.buttonUpEvent.AddListener(OnButtonClickUp);
        btn.buttonReleaseEvent.AddListener(OnButtonClickUp);
    }

    private void Start ()
    {
        GameManager.inst.GameOverAction += PlayCardDisableAnim;
        GameManager.inst.RoundStartAction += CardEnable;
    }

    public void IsClicked ()
    {
        if(GameManager.inst.isCardClickable == true)
        {
            GameManager.inst.OnCardClicked(cardNum);
        }
    }

    private void CardEnable()
    {
        Color white = Color.white;
        white.a = 1;
        btnImg.color = white;
        white.a = 0;
        clickedImg.color = white;
        disabledImg.color = white;
    }

    public void PlayCardBlinkAnim (float duration)
    {
        StartCoroutine(BlinkAnim(duration));
    }

    private void PlayCardDisableAnim()
    {
        StartCoroutine(CardDisableAnim());
    }

    private void PlayClickSound()
    {
        GameManager.inst.PlaySfxSound(SFXSounds.SFX_NA);
    }


    private IEnumerator BlinkAnim (float duration)
    {
        float time = 0;

        float clickedDuration = duration / 4;//0.25f;
        float releasedStartTime = duration / 2;//0.5f;

        float originalImgAlpha;
        float clickedImgAlpha;
        Color originalImgColor = btnImg.color;
        Color clickedImgColor = clickedImg.color;

        GameSoundManager.inst.SetSfxPitch(0.85f + cardNum * 0.025f);
        float animDelta;
        PlayClickSound();
        while (duration > time)
        {
            if (time < clickedDuration)
            {
                animDelta = time / clickedDuration;
                originalImgAlpha = Mathf.Lerp(1, 0, animDelta);
                clickedImgAlpha = Mathf.Lerp(0, 1, animDelta);
                originalImgColor.a = originalImgAlpha;
                clickedImgColor.a = clickedImgAlpha;

                btnImg.color = originalImgColor;
                clickedImg.color = clickedImgColor;
            }
            else if (time >= releasedStartTime)
            {
                animDelta = (time - releasedStartTime) / (duration - releasedStartTime);
                originalImgAlpha = Mathf.Lerp(0, 1, animDelta);
                Debug.Log(animDelta);
                clickedImgAlpha = Mathf.Lerp(1, 0, animDelta);
                originalImgColor.a = originalImgAlpha;
                clickedImgColor.a = clickedImgAlpha;

                btnImg.color = originalImgColor;
                clickedImg.color = clickedImgColor;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        clickedAnimRoutine = null;
    }    

    private IEnumerator CardDisableAnim()
    {
        float duration = 0.5f;
        float time = 0;

        float originalImgAlpha;
        float disabledImgAlpha;

        Color originalImgColor = btnImg.color;
        Color disabledImgColor = disabledImg.color;

        float animDelta;

        while(duration > time)
        {
            btn.interactable = false;

            animDelta = time / duration;

            originalImgAlpha = Mathf.Lerp(0, 1, animDelta);
            disabledImgAlpha = Mathf.Lerp(0, 1, animDelta);

            originalImgColor.a = originalImgAlpha;
            disabledImgColor.a = disabledImgAlpha;

            btnImg.color = originalImgColor;
            disabledImg.color = disabledImgColor;

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnButtonClickDown()
    {
        if(btn.interactable)
        {
            Debug.Log(string.Format("{0} is Press Down.", gameObject.name));
            gameObject.transform.localScale = Vector3.one * 1.05f;
        }
    }

    private void OnButtonClickUp()
    {
        if(btn.interactable)
        {
            Debug.Log(string.Format("{0} is Press Up.", gameObject.name));
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
