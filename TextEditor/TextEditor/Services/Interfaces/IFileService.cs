using System.Windows.Documents;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces
{
    interface IFileService
    {
        void Load(string filename, TextPointer start, TextPointer end);
        void Save(string filename, TextPointer start, TextPointer end);
    }
}
