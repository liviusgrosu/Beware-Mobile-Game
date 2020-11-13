using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;

public class AdInit : MonoBehaviour
{
    private static string adDir;
    private const int numOfAdsPerSize = 3;
    private static bool adsAvailable;
    
    private void Awake()
    {
        adDir = $"{Application.persistentDataPath}/Ads";
    }

    public void VerifyAdFolder()
    {
        bool folderExists = Directory.Exists(adDir);

        if (!folderExists)
        {
            // Create parent directory
            Directory.CreateDirectory(adDir);

            // Create sizes directory
            Directory.CreateDirectory($"{adDir}/Banner");
            Directory.CreateDirectory($"{adDir}/Small");
            Directory.CreateDirectory($"{adDir}/Large");
        }
    }

    public void RefreshLatestAds()
    {
        StartCoroutine(DownloadAllAds());
    }

    private IEnumerator DownloadAllAds()
    {
        foreach (string adSize in Enum.GetNames(typeof(EnumDefinitions.AdSizes)))
        {
            for (int i = 1; i <= 3; i++)
            {
                var storage = FirebaseStorage.DefaultInstance;

                var imageReference = storage.GetReference($"/Ads 2020-10-16/{adSize}/{i}.png");
                var downloadTask = imageReference.GetBytesAsync(long.MaxValue);
                yield return new WaitUntil(() => downloadTask.IsCompleted);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(downloadTask.Result);

                System.IO.File.WriteAllBytes($"{adDir}/{adSize}/{i}.png", texture.EncodeToPNG());
            }
        }
    }
}
