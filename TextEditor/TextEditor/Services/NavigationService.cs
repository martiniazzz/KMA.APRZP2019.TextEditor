using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Services
{
    class NavigationService
    {
        #region static
        /// <summary>
        /// This field is used only in lock to synchronize threads
        /// </summary>
        private static readonly object Lock = new object();
        /// <summary>
        /// Singleton Object of a service
        /// </summary>
        private static NavigationService _instance;

        /// <summary>
        /// Singelton Object of a service
        /// </summary>
        public static NavigationService Instance
        {
            get
            {
                //If object is already initialized, then return it
                if (_instance != null)
                    return _instance;
                //Lock operator for threads synchrnization, in case few threads 
                //will try to initialize Instance at the same time
                lock (Lock)
                {
                    //Initialize Singleton instance and return its object
                    return _instance = new NavigationService();
                }
            }
        }
        #endregion
        /// <summary>
        /// Current NavigationModel field
        /// </summary>
        private NavigationModel _navigationModel;
        /// <summary>
        /// This methos is used to switch to another navigation model
        /// </summary>
        /// <param name="navigationModel">New NavigationModel</param>
        internal void Initialize(NavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }
        /// <summary>
        /// This method performs switch betwean different controls
        /// </summary>
        /// <param name="mode">Enum value of corresponding control</param>
        internal void Navigate(Mode mode)
        {
            //If _navigationModel is null, nothing will happen
            _navigationModel?.Navigate(mode);
            OnNavigateModeChanged(mode);
        }

        #region Events and Handlers
        /// <summary>
        /// Event that occurs when Navigate method (<see cref="Navigate(Mode)"/>) is called 
        /// </summary>
        internal event NavigateModeChangedHandler NavigateModeChanged;
        internal delegate void NavigateModeChangedHandler(Mode mode);
        internal virtual void OnNavigateModeChanged(Mode mode)
        {
            //if not null, event is invoked
            NavigateModeChanged?.Invoke(mode);
        }
        #endregion
    }
}
