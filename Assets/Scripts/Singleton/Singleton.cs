using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static volatile T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
            }
            return _instance;
        }
    }
}
