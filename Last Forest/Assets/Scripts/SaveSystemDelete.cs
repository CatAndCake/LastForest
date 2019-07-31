using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemDelete 
{
    public static void SavePlayer(InfoToSaveDelete infoToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "NewInfo.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveAndLoadDelete data = new SaveAndLoadDelete(infoToSave);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveAndLoadDelete Load()
    {
        string path = Application.persistentDataPath + "NewInfo.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveAndLoadDelete data = formatter.Deserialize(stream) as SaveAndLoadDelete;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save Filenot Found" + path);
            return null; 
        }
    }
}
