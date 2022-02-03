using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Button btn;
    private Image btnImg;
    private Coroutine clickedAnimRoutine = null;

    public int cardNum;

    public Color defaultColor;
    public Color clickedColor;

    private void Awake ()
    {
        btn = GetComponent<Button>();
        btnImg = GetComponent<Image>();
        btn.onClick.AddListener(IsClicked);

        btnImg.color = defaultColor;
    }

    private void Start ()
    {

    }

    public void IsClicked ()
    {
        if(GameManager.inst.isCardClickable == true)
        {
            PlayClickedAnim();

            GameManager.inst.OnCardClicked(cardNum);
        }
    }

    public void PlayClickedAnim ()
    {
        if (clickedAnimRoutine == null)
        {
            clickedAnimRoutine = StartCoroutine(ClickedAnim());
        }
    }

    private IEnumerator ClickedAnim ()
    {
        GameManager.inst.isCardClickable = false;

        float duration = 1;
        float time = 0;

        float clickedDuration = 0.25f;
        float releasedStartTime = 0.5f;

        while (duration > time)
        {
            if (time < clickedDuration)
            {
                btnImg.color = Color.Lerp(defaultColor, clickedColor, time / clickedDuration);
            }
            else if (time >= releasedStartTime)
            {
                btnImg.color = Color.Lerp(clickedColor, defaultColor, (time - releasedStartTime) * (duration / releasedStartTime));
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        btnImg.color = defaultColor;
        clickedAnimRoutine = null;

        GameManager.inst.isCardClickable = true;
    }
}
