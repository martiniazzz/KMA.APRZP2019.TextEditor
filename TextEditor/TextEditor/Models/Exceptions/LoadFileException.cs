using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions
{
    class LoadFileException : Exception
    {
        public LoadFileException(string message) : base(message) { }
    }
}
