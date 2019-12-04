using KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces;
using Microsoft.Win32;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class TextEditorDialogService : IDialogService
    {
        /// <summary>
        /// Returns filepath of file selected from file dialog opened with class methods
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Opens Open File Dialog
        /// </summary>
        /// <returns><c>true</c> if user clicks Open button of dialog, <c>false</c> otherwise </returns>
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Opens Save File Dialog
        /// </summary>
        /// <returns><c>true</c> if user clicks Save button of dialog, <c>false</c> otherwise </returns>
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Shows specified message inside message box
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Shows specified question inside message box
        /// </summary>
        /// <param name="question">Question which can be answered with YES, NO, Cancel</param>
        /// <returns>Answer , selected by user : YES, NO, Cancel</returns>
        public MessageBoxResult ShowYesNoQuestion(string question)
        {
            MessageBoxResult result = MessageBox.Show(question, "Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            return result;
        }
    }
}
