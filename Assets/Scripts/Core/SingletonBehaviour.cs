using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T inst { get; protected set; }

    private void Awake ()
    {
        if (inst == null)
        {
            inst = (T)this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning(string.Format("[{0}] is already loaded \n Destroy [{0}].", this.name));
        }
    }

    protected virtual void Init ()
    {
        Debug.Log(string.Format("[{0}] is succesfully loaded.", this.name));
    }
}
