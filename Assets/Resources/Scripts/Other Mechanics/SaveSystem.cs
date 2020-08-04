using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevel(string levelName, int levelStarScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + levelName + ".ld";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelName, levelStarScore);

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
}
