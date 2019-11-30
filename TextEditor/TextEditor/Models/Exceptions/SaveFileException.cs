using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    /// <summary>
    /// Thrown when error happens during file saving 
    /// </summary>
    class SaveFileException : Exception
    {
        public SaveFileException(string message) : base(message) { }
    }
}
