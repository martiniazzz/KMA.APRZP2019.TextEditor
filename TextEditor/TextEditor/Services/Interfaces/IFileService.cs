using System.Windows.Documents;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces
{
    /// <summary>
    /// Responsible for saving file and retrieving information from file
    /// </summary>
    interface IFileService
    {
        /// <summary>
        /// Retrieve information from file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <param name="start">Pointer to the start of a flow document that will show file content</param>
        /// <param name="end">Pointer to the end of a flow document that will show file content</param>
        void Load(string filename, TextPointer start, TextPointer end);

        /// <summary>
        /// Save information to file
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <param name="start">Pointer to the start of a flow document that is showing some information</param>
        /// <param name="end">Pointer to the end of a flow document that is showing some information</param>
        void Save(string filename, TextPointer start, TextPointer end);
    }
}
