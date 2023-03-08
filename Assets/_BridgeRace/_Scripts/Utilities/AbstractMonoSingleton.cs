using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMonoSingleton <T> : MonoBehaviour where T : AbstractMonoSingleton<T>
{
    private static T s_instance = null;
    static bool shuttingDown = false;
    public static T Instance
    {
        get
        {
            if (s_instance == null && !shuttingDown && Application.isPlaying)
            {
                s_instance = FindObjectOfType(typeof(T)) as T;
                if(s_instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    s_instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
            }

            return s_instance;
        }
    }
    void Awake() => OnInit();

    protected virtual void OnDestroy()
    {
        if (this == s_instance)
        {
            s_instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        s_instance = null;
        shuttingDown = true;
    }
    protected virtual void OnInit()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
        }
        else if (s_instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(gameObject);
            return;
        }
    }
}
