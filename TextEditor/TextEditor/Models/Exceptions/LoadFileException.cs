using System;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    class LoadFileException : Exception
    {
        public LoadFileException(string message) : base(message) { }
    }
}
