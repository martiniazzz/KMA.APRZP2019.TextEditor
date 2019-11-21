using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public class Serializator
    {
        public static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                FileFolderHelper.CheckAndCreateFile(filePath);
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger.Log($"Failed to serialize data to file {filePath}", ex);
                throw;
            }
        }

        public static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    return (TObject)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to Deserialize Data From File {filePath}", ex);
                return null;
            }
        }
    }
}
