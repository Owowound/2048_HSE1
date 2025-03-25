using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileWork : MonoBehaviour
{
    static string filePath = Path.Combine(Application.persistentDataPath, "FileData.dat");

    public static void SaveData(bool[,] taken, int[,] values, int MaxScore)
    {
        GameData data = new GameData(taken, values, MaxScore);

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static GameData LoadData()
    {
        GameData data = new GameData();
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as GameData;
            }
        }
        else
        {
            Debug.LogError("Файл не найден");
        }
        return data;
    }
}
