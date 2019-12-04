using KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class RtfFileService : IFileService
    {
        /// <summary>
        /// Load formatted text file (rtf) content between two pointers of FlowDocument 
        /// </summary>
        /// <param name="filename">Filepath to file that will be loaded</param>
        /// <param name="start">Start pointer of FlowDoument</param>
        /// <param name="end">End pointer of FlowDocument</param>
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
                Logger.Log(ex);
                throw new LoadFileException(String.Format(Resources.LoadFileException_Msg, filename));
            }
        }

        /// <summary>
        /// Saves formatted content between two pointerts of FlowDocument to rtf file
        /// </summary>
        /// <param name="filename">Filepath of new or existing file where formatted text will be saved</param>
        /// <param name="start">Start pointer of FlowDoument</param>
        /// <param name="end">End pointer of FlowDocument</param>
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
                Logger.Log(ex);
                throw new SaveFileException(String.Format(Resources.SaveFileException_Msg, filename));
            }
        }
    }
}
