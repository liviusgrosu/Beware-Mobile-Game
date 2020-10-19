using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
public class DownloadAds : MonoBehaviour
{
    public Image bannerAdSpot, smallAdSpot, largeAdSpot;

    public void StartDownload()
    {
        StartCoroutine(DownloadCoroutine("Banner"));
        //StartCoroutine(DownloadCoroutine("Small"));
        //StartCoroutine(DownloadCoroutine("Large"));
    }

    private IEnumerator DownloadCoroutine(string adType)
    {
        var storage = FirebaseStorage.DefaultInstance;
        var imageReference = storage.GetReference($"/Ads 2020-10-16/{adType}/{Random.Range(1, 4)}.png");

        var downloadTask = imageReference.GetBytesAsync(long.MaxValue);
        yield return new WaitUntil(() => downloadTask.IsCompleted);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(downloadTask.Result);
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite imgSprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        switch(adType)
        {
            case "Banner":
                bannerAdSpot.sprite = imgSprite;
                break;
            case "Small":
                smallAdSpot.sprite = imgSprite;
                break;
            case "Large":
                largeAdSpot.sprite = imgSprite;
                break;
        }
        
    }
}
