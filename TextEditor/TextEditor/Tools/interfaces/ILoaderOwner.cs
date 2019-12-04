using System.ComponentModel;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces
{
    /// <summary>
    /// Repsents elements that have loader
    /// </summary>
    internal interface ILoaderOwner: INotifyPropertyChanged
    {
        /// <summary>
        /// Gets loader visibility
        /// </summary>
        Visibility LoaderVisibility { get; set; }

        /// <summary>
        /// Gets property that is specifies whether content is enabled
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
