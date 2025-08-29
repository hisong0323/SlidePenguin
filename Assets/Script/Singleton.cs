using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance;

    [SerializeField]
    private bool donDestroy = false;

    protected virtual void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        if (donDestroy)
            DontDestroyOnLoad(gameObject);

        _instance = this as T;
    }

}
