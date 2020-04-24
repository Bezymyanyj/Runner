using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAsComponent<T> : MonoBehaviour where T : SingletonAsComponent<T>
{
    private static T Instance;
    private bool _alive = true;

    protected static SingletonAsComponent<T> _Instance
    {
        get
        {
            if (!Instance)
            {
                T[] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];
                if (managers != null)
                {
                    if (managers.Length == 1)
                    {
                        Instance = managers[0];
                        return Instance;
                    }
                    else if (managers.Length > 1)
                    {
                        Debug.LogError("You have more than one " +
                                        typeof(T).Name +
                                        " in the Scene. You only need " +
                                        "one - it's a singleton!");
                        for (int i = 0; i < managers.Length; ++i)
                        {
                            T manager = managers[i];
                            Destroy(manager.gameObject);
                        }
                    }
                }
                GameObject go = new GameObject(typeof(T).Name, typeof(T));
                Instance = go.GetComponent<T>();
                DontDestroyOnLoad(Instance.gameObject);
            }
            return Instance;
        }
        set
        {
            Instance = value as T;
        }
    }

    void OnDestroy()
    {
        _alive = false;
    }

    void OnApplicationQuit()
    {
        _alive = false;
    }

    public static bool IsAlive
    {
        get
        {
            if (Instance == null)
                return false;
            return Instance._alive;
        }
    }
}
