using System.ComponentModel;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces
{
    internal interface ILoaderOwner: INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
    }
}
