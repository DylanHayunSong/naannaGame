using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTextManager : MonoBehaviour
{
    [SerializeField]
    private Text roundText;

    private void Start ()
    {
        GameManager.inst.RoundStartAction += () => { roundText.text = (GameManager.inst.currentRound + 1).ToString(); };
    }
}
