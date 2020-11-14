using UnityEngine;

public class AppCloseCheck : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("initial_app_open", 1);
    }
}
