using UnityEngine;

/// <summary>
/// This is lazy implementation of Singleton Design Pattern.
/// Instance is created when someone call Instance property.
/// Additionally, with this implementation you have same instance when moving to different scene.
/// </summary>
public class PersistentLazySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Flag used to mark singleton destruction.
    private static bool singletonDestroyed = false;

    // Reference to our singular instance.
    private static T instance;

    public static T Instance
    {
        get
        {
            if (singletonDestroyed) // If game is closing and we already destroyed instance, we shouldn't create new one!
            {
                Debug.LogWarningFormat("[Singleton] Singleton was already destroyed by quiting game. Returning null");
                return null;
            }

            if (!instance) // If there is no object already, we should create new one.
            {
                // Creating new game object with singleton component.
                // We don't need to assign reference here as Awake() will be called immediately after coponent is added.
                new GameObject(typeof(T).ToString()).AddComponent<T>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Unity method called just after object creation - like constructor.
    /// </summary>
    protected virtual void Awake()
    {
        // If we don't have reference to instance and we didn't destroy instance yet than this object will take control
        if (instance == null && !singletonDestroyed)
        {
            instance = this as T;
            // Now we are making sure that object won't be destroy when we will move to other scene.
            DontDestroyOnLoad(instance);
        }
        else if (instance != this || FindObjectsOfType<T>().Length > 1) // Else this is other instance and we should destroy it!
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Unity method called before object destruction.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (instance != this) // Skip if instance is other than this object.
        {
            return;
        }

        singletonDestroyed = true;
        instance = null;
    }
}