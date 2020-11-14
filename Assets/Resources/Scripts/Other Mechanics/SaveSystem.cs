using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    private static string adDir = $"{Application.persistentDataPath}/Ads";

    public static void SaveLevel(string levelName, bool levelFinished, int levelStarScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + levelName + ".ld";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelName, levelFinished, levelStarScore);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static LevelData LoadLevel(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".ld";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelData data = formatter.Deserialize(stream) as LevelData;

            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveSettings(float soundLevel)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.sd";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingData data = new SettingData(soundLevel);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SettingData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.sd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SettingData data = formatter.Deserialize(stream) as SettingData;

            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static Sprite LoadAd(EnumDefinitions.AdSizes adSize)
    {
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
