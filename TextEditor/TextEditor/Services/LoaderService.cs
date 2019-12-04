using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using System.Windows;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    /// <summary>
    /// Controls showing loader on screen
    /// </summary>
    class LoaderService
    {
        /// <summary>
        /// lock for case of multiple threads
        /// </summary>
        private static readonly object Lock = new object();
        private static LoaderService _instance;

        /// <summary>
        /// Singleton object of the service
        /// </summary>
        internal static LoaderService Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                //Lock operator for threads synchrnization, in case few threads 
                //will try to initialize Instance at the same time
                lock (Lock)
                {
                    return _instance = new LoaderService();
                }
            }
        }

        private ILoaderOwner _loaderOwner;

        /// <summary>
        /// Supplies loader owner which loader will be controlled by service
        /// </summary>
        /// <param name="loaderOwner">Object which has loader</param>
        internal void Initialize(ILoaderOwner loaderOwner)
        {
            _loaderOwner = loaderOwner;
        }

        /// <summary>
        /// Makes loader appear on loader owner surface
        /// </summary>
        internal void ShowLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Visible;
            _loaderOwner.IsEnabled = false;

        }

        /// <summary>
        /// Hides loader from loader owner surface
        /// </summary>
        internal void HideLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Hidden;
            _loaderOwner.IsEnabled = true;
        }
    }
}
