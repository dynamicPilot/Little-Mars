using LittleMars.AudioSystems;
using System.IO;
using UnityEngine;

namespace LittleMars.SaveSystem
{
    public class PathChecker
    {
        string _path = Application.persistentDataPath + "/";

        public bool CheckFile(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public bool CheckFolder(string path, bool createIfNot)
        {
            
            var fullPath = string.Concat(_path, path);
            var isExist = Directory.Exists(fullPath);

            if (!isExist && createIfNot)
            {
                Directory.CreateDirectory(fullPath);
                isExist = Directory.Exists(fullPath);
                Debug.Log("PathChecker: create folder " + fullPath + " is exist " + isExist);
            }

            return isExist;
        }
    }
}
