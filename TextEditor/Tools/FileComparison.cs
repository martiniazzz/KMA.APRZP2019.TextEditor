using System;
using System.IO;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public static class FileComparison
    {
        /// <summary>
        /// Compares specified <paramref name="text"/> to content of file <paramref name="filename"/>
        /// </summary>
        /// <param name="text">Text to compare with content of <paramref name="filepath"/> </param>
        /// <param name="filename">File which content wil be compared to <paramref name="text"/></param>
        /// <returns><c>true</c> if <paramref name="text"/> equals to content of <paramref name="filename"/>, <c>false</c> otherwise</returns>
        public static bool EqualsTextToFileText(string text, string filename)
        {
            if (!File.Exists(filename))
                return false;
            using (StreamReader sr = new StreamReader(filename))
            {
                try
                {
                    string textLine = null;
                    string fileLine = null;
                    for (int i = 0; i < text.Length-1 && (fileLine = sr.ReadLine()) != null;)
                    {
                        int newLineInd = text.IndexOf(Environment.NewLine, i, text.Length - i);
                        textLine = text.Substring(i, newLineInd - i);
                        i = newLineInd + Environment.NewLine.Length;
                        if (textLine != fileLine)
                            return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            }
        }

    }
}
