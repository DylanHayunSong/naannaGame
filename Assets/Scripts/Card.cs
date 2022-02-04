using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Button btn;
    [SerializeField]
    private Image btnImg;
    [SerializeField]
    private Image clickedImg;
    private Coroutine clickedAnimRoutine = null;

    public int cardNum;

    public Color defaultColor;
    public Color clickedColor;

    private void Awake ()
    {
        btn.onClick.AddListener(IsClicked);

        //btnImg.color = defaultColor;
    }

    private void Start ()
    {

    }

    public void IsClicked ()
    {
        if(GameManager.inst.isCardClickable == true)
        {
            GameManager.inst.OnCardClicked(cardNum);
        }
    }

    public void PlayCardBlinkAnim ()
    {
        StartCoroutine(ClickedAnim());
    }

    private IEnumerator ClickedAnim ()
    {
        float duration = 1;
        float time = 0;

        float clickedDuration = 0.25f;
        float releasedStartTime = 0.5f;

        float originalImgAlpha = 0;
        float clickedImgAlpha = 0;
        Color originalImgColor = btnImg.color;
        Color clickedImgColor = clickedImg.color;

        float t;
        while (duration > time)
        {
            if (time < clickedDuration)
            {
                t = time / clickedDuration;
                //btnImg.color = Color.Lerp(defaultColor, clickedColor, t);
                originalImgAlpha = Mathf.Lerp(1, 0, t);
                clickedImgAlpha = Mathf.Lerp(0, 1, t);
                originalImgColor.a = originalImgAlpha;
                clickedImgColor.a = clickedImgAlpha;

                btnImg.color = originalImgColor;
                clickedImg.color = clickedImgColor;
            }
            else if (time >= releasedStartTime)
            {
                t = (time - releasedStartTime) * (duration / releasedStartTime);
                //btnImg.color = Color.Lerp(clickedColor, defaultColor, (time - releasedStartTime) * (duration / releasedStartTime));
                originalImgAlpha = Mathf.Lerp(0, 1, t);
                clickedImgAlpha = Mathf.Lerp(1, 0, t);
                originalImgColor.a = originalImgAlpha;
                clickedImgColor.a = clickedImgAlpha;

                btnImg.color = originalImgColor;
                clickedImg.color = clickedImgColor;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //btnImg.color = defaultColor;
        clickedAnimRoutine = null;
    }
}
