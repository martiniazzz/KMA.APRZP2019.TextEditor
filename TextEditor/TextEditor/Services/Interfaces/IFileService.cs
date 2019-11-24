using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces
{
    interface IFileService
    {
        void Load(string filename, TextPointer start, TextPointer end);
        void Save(string filename, TextPointer start, TextPointer end);
    }
}
