using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces
{
    internal interface ILoaderOwner
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
    }
}
