using KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class RtfFileService : IFileService
    {
        public void Load(string filename, TextPointer start, TextPointer end)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    TextRange range = new TextRange(start, end);
                    range.Load(fileStream, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new LoadFileException(String.Format(Resources.LoadFileException_Msg, filename));
            }
        }

        public void Save(string filename, TextPointer start, TextPointer end)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    TextRange range = new TextRange(start, end);
                    range.Save(fileStream, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SaveFileException(String.Format(Resources.SaveFileException_Msg, filename));
            }
        }
    }
}
