using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    //[SerializeField]
    //private string playerSession;
    [SerializeField]
    private string gameStartTime;
    [SerializeField]
    private string gameEndTime;
    [SerializeField]
    private int lastLevel;
    //[SerializeField]
    //private Texture resultCapture;

    private void Start ()
    {
        GameManager.inst.SetGameStartTimeAction += (value) => gameStartTime = value;
        GameManager.inst.SetGameEndTimeAction += (value) => gameEndTime = value;
        GameManager.inst.SetLastLevelAction += (value) => lastLevel = value;
    }

    public string SerializeUserData()
    {
        string result = JsonUtility.ToJson(inst);

        return result;
    }

}
