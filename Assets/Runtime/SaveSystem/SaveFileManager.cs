using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Runtime.SaveSystem
{
    public static class SaveFileManager
    {
        private static string SavesDirectory => Path.Combine(Application.persistentDataPath, "Saves");
        
        public class SaveFileInfo
        {
            public string FileName { get; set; }
            public string DisplayName { get; set; }
            public DateTime LastModified { get; set; }
            public string FullPath { get; set; }
        }
        
        public static void Initialize()
        {
            if (!Directory.Exists(SavesDirectory))
            {
                Directory.CreateDirectory(SavesDirectory);
            }
        }
        
        public static string GetSaveFilePath(string saveName)
        {
            Initialize();
            
            var fileName = SanitizeFileName(saveName) + ".json";
            return Path.Combine(SavesDirectory, fileName);
        }
        
        public static bool SaveExists(string saveName)
        {
            return File.Exists(GetSaveFilePath(saveName));
        }
        
        public static List<SaveFileInfo> GetAllSaves()
        {
            Initialize();

            var saveFiles = new List<SaveFileInfo>();

            if (!Directory.Exists(SavesDirectory))
            {
                return saveFiles;
            }

            var files = Directory.GetFiles(SavesDirectory, "*.json");

            foreach (var filePath in files)
            {
                var fileInfo = new FileInfo(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                saveFiles.Add(new SaveFileInfo
                {
                    FileName = fileName,
                    DisplayName = fileName,
                    LastModified = fileInfo.LastWriteTime,
                    FullPath = filePath
                });
            }
            
            return saveFiles.OrderByDescending(s => s.LastModified).ToList();
        }
        
        public static void DeleteSave(string saveName)
        {
            var filePath = GetSaveFilePath(saveName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        private static string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
            
            if (string.IsNullOrWhiteSpace(sanitized))
            {
                sanitized = "save_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            }

            return sanitized;
        }
        
        public static string GenerateUniqueSaveName(string baseName)
        {
            var saveName = baseName;
            var counter = 1;

            while (SaveExists(saveName))
            {
                saveName = $"{baseName}_{counter}";
                counter++;
            }

            return saveName;
        }
    }
}
