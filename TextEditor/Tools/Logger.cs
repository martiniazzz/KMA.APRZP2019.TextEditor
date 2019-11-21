﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public class Logger
    {
        public static void Log(string message)
        {
            lock (FileFolderHelper.LogFilepath)
            {
                StreamWriter writer = null;
                FileStream file = null;
                try
                {
                    FileFolderHelper.CheckAndCreateFile(FileFolderHelper.LogFilepath);
                    file = new FileStream(FileFolderHelper.LogFilepath, FileMode.Append);
                    writer = new StreamWriter(file);
                    writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    writer?.Close();
                    file?.Close();
                    writer = null;
                    file = null;
                }
            }
        }
        public static void Log(string message, Exception ex)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(message);
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }

        public static void Log(Exception ex)
        {
            var stringBuilder = new StringBuilder();
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }
    }
}
