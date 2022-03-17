using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;

public class RequestManager : SingletonBehaviour<RequestManager>
{
    [SerializeField]
    [TextArea]
    private string bodyTest;

    private void Start ()
    {
        PostGameStart("", "");
    }

    public void PostGameStart (string memNicnm, string memId)
    {
        if (string.IsNullOrEmpty(memNicnm))
        {
            memNicnm = "aiden";
        }

        if (string.IsNullOrEmpty(memId))
        {
            memId = "admin";
        }

        string uri = "http://naanna.kr/ajax.game.php";
        string headerKey = "Content-Type";
        string headerValue = "application/json";
        string body = "" +
            "{ \n" +
            "\"acces_type\" : \"i\", \n" +
            "\"game_no\" : \"1\", \n" +
            "\"mb_nicknm\" : \"" + memNicnm + "\", \n" +
            "\"mb_id\" : \"" + memId + "\" \n" +
            "}";

        print(body);

        PostRequest(uri, headerKey, headerValue, bodyTest, (value) => { Debug.Log(value); });
    }

    public void PostGameOver (string gameStep, string memNicknm, string memId)
    {

    }

    private void PostRequest (string uri, string headerKey, string headerValue, string body, Action<string> OnReqDone)
    {
        StartCoroutine(PostRoutine(uri, headerKey, headerValue, body, OnReqDone));
    }

    private IEnumerator PostRoutine (string uri, string headerKey, string headerValue, string body, Action<string> OnReqDone)
    {
        UnityWebRequest request = UnityWebRequest.Post(uri, body);
        request.SetRequestHeader(headerKey, headerValue);

        Debug.Log(string.Format("uri : {0} \n" +
            "headerKey : {1} \n" +
            "headerValue : {2} \n" +
            "body : {3} \n", uri, headerKey, headerValue, body));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (OnReqDone != null)
            {
                JObject decoded = JObject.Parse(request.downloadHandler.text);

                OnReqDone.Invoke(decoded.ToString());
            }
        }

        yield return null;
    }
}
