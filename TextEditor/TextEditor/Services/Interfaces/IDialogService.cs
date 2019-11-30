using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces
{
    /// <summary>
    /// Responsible for showing dialog windows to user and getting information entered by user
    /// </summary>
    interface IDialogService
    {
        /// <summary>
        /// Shows simple message box with specified message in it
        /// </summary>
        /// <param name="message">Message to show</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows user a question which can be responded by YES, NO or CANCEL
        /// </summary>
        /// <param name="question">Question to show</param>
        /// <returns>User answer (YES, NO or CANCEL)</returns>
        MessageBoxResult ShowYesNoQuestion(string question);

        /// <summary>
        /// Get file path selected by user from dialog window
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Show open file dialog
        /// </summary>
        /// <returns><c>true</c> if user clicks Open button, <c>false</c> otherwise</returns>
        bool OpenFileDialog(); 

        /// <summary>
        /// Show save file dialog
        /// </summary>
        /// <returns><c>true</c> if user clicks Save button, <c>false</c> otherwise</returns>
        bool SaveFileDialog();
    }
}
