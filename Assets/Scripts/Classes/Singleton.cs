using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T: Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var objects = FindObjectsOfType(typeof(T)) as T[];

                if (objects.Length > 0)
                {
                    instance = objects[0];
                }

                if (objects.Length > 1)
                {
                    Debug.LogError("Singleton: more that one object of " + typeof(T).Name);
                }

                if (instance == null)
                {
                    // create
                    GameObject gameObject = new GameObject();
                    gameObject.hideFlags = HideFlags.HideAndDontSave;
                    instance = gameObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

}


public class SingletonPersistent<T>: MonoBehaviour
    where T: Component
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
