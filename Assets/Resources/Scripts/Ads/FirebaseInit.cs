using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    private async void Start()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            OnFirebaseInitialized.Invoke();
        }
    }
}
