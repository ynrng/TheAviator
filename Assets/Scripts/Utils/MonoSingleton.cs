
using UnityEngine;
using UnityEngine.InputSystem;

// Use a separate InputControl component for setting up input.
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    private static T _instance;
    public static T Instance {
        get {
            if (_instance == null) {
                Debug.LogError(typeof(T).ToString() + " is null.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
    }

}
