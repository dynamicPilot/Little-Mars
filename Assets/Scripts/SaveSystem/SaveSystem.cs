using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string subFolder = "Saves";
    private static string mainSaveFileName = "data.sav";
    private static string backUpSaveFileName = "data_.sav";
    public static void SaveData(int newCurrentLevelIndex, int[] newCompletedLevels)
    {
        // check folder
        CheckMainFolderToSave();

        // do back up
        ChangeMainToBackUp();

        string path = Application.persistentDataPath + "/" + subFolder + "/" + mainSaveFileName;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(newCurrentLevelIndex, newCompletedLevels);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/" + subFolder + "/";

        CheckMainFolderToSave();
        if (File.Exists(path + mainSaveFileName))
        {
            return LoadMainOrBackUpData(path + mainSaveFileName);
        }
        else if (File.Exists(path + backUpSaveFileName))
        {
            ChangeBackUpToMain();
            return LoadMainOrBackUpData(path + mainSaveFileName);
        }
        else
        {
            Debug.Log("SaveSystem: save file for BasicData not found in " + path);
            return null;
        }
    }

    static GameData LoadMainOrBackUpData(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        GameData data = formatter.Deserialize(stream) as GameData;
        stream.Close();

        return data;
    }

    static void CheckMainFolderToSave()
    {
        bool isExist = Directory.Exists(Application.persistentDataPath + "/" + subFolder);

        if (!isExist)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + subFolder);
        }
    }

    static void ChangeMainToBackUp()
    {
        string path = Application.persistentDataPath + "/" + subFolder + "/" + mainSaveFileName,
            newPath = Application.persistentDataPath + "/" + subFolder + "/" + backUpSaveFileName,
            copyPath = Application.persistentDataPath + "/" + subFolder + "/" + "data_back.sav";

        if (File.Exists(path) && File.Exists(newPath))
        {
            // rename file
            File.Replace(path, newPath, copyPath);
        }
        else if (File.Exists(path))
        {
            File.Move(path, newPath);
        }
    }

    static void ChangeBackUpToMain()
    {
        string path = Application.persistentDataPath + "/" + subFolder + "/" + backUpSaveFileName,
            newPath = Application.persistentDataPath + "/" + subFolder + "/" + mainSaveFileName;

        if (File.Exists(path))
        {
            // rename file
            File.Copy(path, newPath);
        }
    }
}
