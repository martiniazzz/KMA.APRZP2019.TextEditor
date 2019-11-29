using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces
{
    interface IDialogService
    {
        void ShowMessage(string message);
        MessageBoxResult ShowYesNoQuestion(string question);
        string FilePath { get; set; }
        bool OpenFileDialog(); 
        bool SaveFileDialog();
    }
}
