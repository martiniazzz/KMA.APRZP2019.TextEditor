using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    /// <summary>
    /// Thrown when error happens during file saving 
    /// </summary>
    class SaveFileException : Exception
    {
        /// <summary>
        /// Exception thrown when error happens during file saving 
        /// </summary>
        /// <param name="message">exception message</param>
        public SaveFileException(string message) : base(message) { }
    }
}
