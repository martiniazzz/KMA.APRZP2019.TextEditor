using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "TextEditor");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        public static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.textedit");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.textedit");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {
                Logger.Log("Failed to create file " + filePath);
                throw;
            }
        }

        public static void CheckAndDeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath)){
                    File.Delete(filePath);
                }
            }
            catch (Exception)
            {
                Logger.Log("Failed to delete file "+filePath);
                throw;
            }
        }
    }
}
