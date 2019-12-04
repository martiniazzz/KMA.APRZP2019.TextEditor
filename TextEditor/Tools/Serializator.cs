using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public class Serializator
    {
        /// <summary>
        /// Serialize object <paramref name="obj"/> to file <paramref name="filePath"/>
        /// </summary>
        /// <typeparam name="TObject">Serializable type</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <param name="filePath">File to which object will be serialized</param>
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
                Logger.Log($"Failed to serialize data to file {filePath}", ex);
                throw;
            }
        }

        /// <summary>
        /// If <paramref name="obj"/> is null, deletes file <paramref name="filePath"/>, otherwise serialize <paramref name="obj"/> to file <paramref name="filePath"/>
        /// </summary>
        /// <typeparam name="TObject">Serializable type</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <param name="filePath">File to which object will be serialized</param>
        public static void SerializeOrDeleteFile<TObject>(TObject obj, string filePath)
        {
            try
            {
                if (obj == null)
                {
                    FileFolderHelper.CheckAndDeleteFile(filePath);
                }
                else
                {
                    Serialize(obj, filePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deserializes object of type <typeparamref name="TObject"/> from file <paramref name="filePath"/>
        /// </summary>
        /// <typeparam name="TObject">Serializable type</typeparam>
        /// <param name="filePath">Path to file from which object will be deserialized</param>
        /// <returns></returns>
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
