using UnityEngine;

namespace LittleMars.SaveSystem
{
    public class SavesSystemPathConstructor
    {
        readonly SavesSystemManager.Settings _settings;
        readonly PathChecker _checker;

        string _mainPath = "";
        string _backupPath = "";

        public SavesSystemPathConstructor(SavesSystemManager.Settings settings, PathChecker checker)
        {
            _settings = settings;
            _checker = checker;
        }

        public bool TryGetMainPath(out string path)
        {
            if (_mainPath == "")
            {
                _checker.CheckFolder(_settings.FolderName, true);
                _mainPath = FormPathToFile(_settings.FileName, _settings.JsonFileEx);
            }

            path = _mainPath;
            return _checker.CheckFile(_mainPath);
        }
        public bool TryGetBinaryPath(out string path)
        {
            _checker.CheckFolder(_settings.FolderName, true);
            path = FormPathToFile(_settings.FileName, _settings.BinaryFileEx);
            return _checker.CheckFile(path);
        }

        public bool TryGetBackupBinaryPath(out string path)
        {
            _checker.CheckFolder(_settings.FolderName, true);
            path = FormPathToFile(_settings.BackupFileName, _settings.BinaryFileEx);
            return _checker.CheckFile(path);
        }

        string FormPathToFile(string fileName, string extend)
        {
            var path = string.Join("/", new string[3] { Application.persistentDataPath, _settings.FolderName, fileName });
            return string.Concat(path, extend);
        }
    }
}
