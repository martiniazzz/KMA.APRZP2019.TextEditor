using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class LoaderService
    {
        private static readonly object Lock = new object();
        private static LoaderService _instance;

        internal static LoaderService Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Lock)
                {
                    return _instance = new LoaderService();
                }
            }
        }

        private ILoaderOwner _loaderOwner;

        internal void Initialize(ILoaderOwner loaderOwner)
        {
            _loaderOwner = loaderOwner;
        }

        internal void ShowLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Visible;
            _loaderOwner.IsEnabled = false;

        }

        internal void HideLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Hidden;
            _loaderOwner.IsEnabled = true;
        }
    }
}
