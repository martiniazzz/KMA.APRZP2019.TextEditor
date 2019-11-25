using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.Tools
{
    public static class FileComparison
    {
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
