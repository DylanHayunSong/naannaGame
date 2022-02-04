using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameSoundBtn : MonoBehaviour
{
    private Image btnImg;
    private Button btn;

    [SerializeField]
    private Sprite soundOnImg;
    [SerializeField]
    private Sprite soundOffImg;

    private void Start ()
    {
        btnImg = GetComponent<Image>();
        btn = GetComponent<Button>();

        btn.onClick.AddListener(OnClick);
    }

    private void OnClick ()
    {
        btnImg.sprite = GameManager.inst.isSoundOn ? soundOffImg : soundOnImg;
        GameManager.inst.isSoundOn = !GameManager.inst.isSoundOn;
    }
}
