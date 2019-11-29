using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    class SaveFileException : Exception
    {
        public SaveFileException(string message) : base(message) { }
    }
}
