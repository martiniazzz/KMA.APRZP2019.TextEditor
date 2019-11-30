using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    /// <summary>
    /// Thrown when error happens during retriving information from file
    /// </summary>
    class LoadFileException : Exception
    {
        public LoadFileException(string message) : base(message) { }
    }
}
