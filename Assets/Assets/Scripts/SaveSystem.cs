using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore () {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.DINOSAUR";

        FileStream stream = new FileStream(path, FileMode.Create);

        HighscoorData data = new HighscoorData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static HighscoorData LoadScore() {
        string path = Application.persistentDataPath + "/score.DINOSAUR";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            formatter.Deserialize(stream);

            HighscoorData data = formatter.Deserialize(stream) as HighscoorData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
