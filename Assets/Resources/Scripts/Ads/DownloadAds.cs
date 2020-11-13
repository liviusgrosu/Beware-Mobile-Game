using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
public class DownloadAds : MonoBehaviour
{
    private const int numOfAdsPerSize = 3;
    public void StartDownload()
    {
        StartCoroutine(DownloadAllAds());
    }

    private IEnumerator DownloadAllAds()
    {

        var storage = FirebaseStorage.DefaultInstance;

        foreach (EnumDefinitions.AdSizes adSize in (EnumDefinitions.AdSizes[]) Enum.GetValues(typeof(EnumDefinitions.AdSizes))) {
            for (int j = 0; j < numOfAdsPerSize; j++)
            {
                var imageReference = storage.GetReference($"/Ads 2020-10-16/{adSize.ToString()}/{j}.png");

                var downloadTask = imageReference.GetBytesAsync(long.MaxValue);
                yield return new WaitUntil(() => downloadTask.IsCompleted);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(downloadTask.Result);
                Rect rec = new Rect(0, 0, texture.width, texture.height);
                Sprite imgSprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
            }
        }
    }
}
