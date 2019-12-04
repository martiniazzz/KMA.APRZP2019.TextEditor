using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    /// <summary>
    /// Thrown when error happens during retriving information from file
    /// </summary>
    class LoadFileException : Exception
    {
        /// <summary>
        /// Exception thrown when error happens during retriving information from file
        /// </summary>
        /// <param name="message">exception message</param>
        public LoadFileException(string message) : base(message) { }
    }
}
