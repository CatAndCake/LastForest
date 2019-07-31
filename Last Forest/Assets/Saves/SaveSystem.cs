using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveSystem
{
    public static void SaveName<T>(T objectToSave, string key)
    {
        SaveNameToFile<T>(objectToSave, key);
    }

    public static void SaveName(Object objectToSave, string key)
    {
        SaveNameToFile<Object>(objectToSave, key);
    }


    private static void SaveNameToFile<T>(T objectToSave, string fileName)
    {
        // Set the path to the persistent data path (works on most devices by default)
        string path = Application.persistentDataPath + "/saves/";

        // Create the directory IF it doesn't already exist
        Directory.CreateDirectory(path);

        // Grab an instance of the BinaryFormatter that will handle serializing our data
        BinaryFormatter formatter = new BinaryFormatter();

        // Open up a filestream, combining the path and object key
        FileStream fileStream = new FileStream(path + fileName + ".txt", FileMode.Create);

        // Try/Catch/Finally block that will attempt to serialize/write-to-stream, closing stream when complete
        try
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Save failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }

    public static T DeleteSave<T>(string key)
    {
        string path = Application.persistentDataPath + "/saves/";


        BinaryFormatter formatter = new BinaryFormatter();

        string saveName = path + key + ".txt";

        FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open);

        

        T returnValue = default(T);
        
        try

        {
            File.Delete(saveName);
            
        }
        catch (SerializationException exception)
        {
            Debug.Log(exception);
        }

        return returnValue;
    }

    

    public static void Delete(string key)
    {
        // Files to be deleted    
        string fileName = Application.persistentDataPath + "/saves/" + key + ".txt";

        try
        {
            // Check if file exists with its full path    
            if (File.Exists(fileName))
            {
                // If file found, delete it
                Debug.Log("File found");
                File.Delete(fileName);
                
            }
            else Debug.Log("File not found");
        }
        catch (IOException ioExp)
        {
            Debug.Log(ioExp.Message);
        }
        if (File.Exists(fileName))
        {
            Debug.Log("still exists");
        }
        else
        {
            Debug.Log("doesn't exists");
        }
    }

    public static T LoadSaveNames<T>(string key)
    {
        
        string path = Application.persistentDataPath + "/saves/";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open);
        T returnValue = default(T);

        try
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Load failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }

        return returnValue;
    }

    public static void SaveGame<T>(T objectToSave, string type, string number)
    {
        SaveGameToFile<T>(objectToSave, type, number);
    }

    public static void SaveGame(Object objectToSave, string type, string number)
    {
        SaveGameToFile<Object>(objectToSave, type, number);
    }
    

    private static void SaveGameToFile<T>(T objectToSave, string fileType, string saveNumber)
    {
        
        string path = Application.persistentDataPath + "/saves/";

        
        Directory.CreateDirectory(path);

       
        BinaryFormatter formatter = new BinaryFormatter();

        
        FileStream fileStream = new FileStream(path + fileType + saveNumber + ".txt", FileMode.Create);

        
        try
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Save failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }


    public static T LoadGame<T>(string type, string number)
    {
        
        string path = Application.persistentDataPath + "/saves/";

        
        BinaryFormatter formatter = new BinaryFormatter();

        
        FileStream fileStream = new FileStream(path + type + number + ".txt", FileMode.Open);

        
        T returnValue = default(T);

        try
        {
            returnValue = (T)formatter.Deserialize(fileStream);
            //Debug.Log(returnValue);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Load failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }

        return returnValue;
    }
}
