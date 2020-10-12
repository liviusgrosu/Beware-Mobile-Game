using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AdController
{
    private static string tempImagesDir = $"{Application.dataPath}/Ads";
    private static string adDir = $"{Application.persistentDataPath}/Ads";

    private static bool adsAvailable;

    public static void VerifyAdFolder()
    {
        bool folderExists = Directory.Exists(adDir);

        if (!folderExists)
        {
            // Create parent directory
            Directory.CreateDirectory(adDir);

            // Create sizes directory
            Directory.CreateDirectory($"{adDir}/banner");
            Directory.CreateDirectory($"{adDir}/small");
            Directory.CreateDirectory($"{adDir}/large");
        }
    }

    public static void RefreshLatestAds()
    {
        TransferPublicToLocalAds("banner");
        TransferPublicToLocalAds("small");
        TransferPublicToLocalAds("large");
    }

    public static void TransferPublicToLocalAds(string subDir)
    {
        //TODO: error checking
        DirectoryInfo di = new DirectoryInfo($"{tempImagesDir}/{subDir}");

        foreach(FileInfo entry in di.GetFiles("*.png"))
        {
            string fp = $"{adDir}/{subDir}/{entry.Name}";
            if (!File.Exists(fp))
            {
                File.Copy(entry.FullName, fp);
            }
        }
    }

    public static Sprite GetAd(EnumDefinitions.AdSizes adSize)
    {
        // Get a random file from the specified size
        DirectoryInfo di = new DirectoryInfo($"{adDir}/{adSize.ToString()}");
        FileInfo [] files = di.GetFiles("*.png");
        string fileDir = files[Random.Range(0, files.Length)].FullName;

        byte[] fileData = File.ReadAllBytes(fileDir);
        Texture2D textureData = new Texture2D(512, 512);
        textureData.LoadImage(fileData);

        Sprite spriteData = Sprite.Create(
            textureData, 
            new Rect(0, 0, textureData.width, textureData.height), 
            new Vector2(0, 0), 
            1);
        return spriteData;
    }
}
