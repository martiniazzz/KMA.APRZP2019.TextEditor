using KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces;
using Microsoft.Win32;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class TextEditorDialogService : IDialogService
    {
        public string FilePath { get; set; }

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

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public MessageBoxResult ShowYesNoQuestion(string question)
        {
            MessageBoxResult result = MessageBox.Show(question, "Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            return result;
        }
    }
}
