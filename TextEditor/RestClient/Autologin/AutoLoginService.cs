using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMA.APRZP2019.TextEditorProject.RestClient.Autologin
{
    /// <summary>
    /// Automatically logins user from file
    /// </summary>
    public class AutoLoginService : INotifyPropertyChanged
    {
        #region static
        /// <summary>
        /// This field is used only in lock to synchronize threads
        /// </summary>
        private static readonly object Lock = new object();
        /// <summary>
        /// Singleton Object of a service
        /// </summary>
        private static AutoLoginService _instance;

        /// <summary>
        /// Singelton Object of a service
        /// </summary>
        public static AutoLoginService Instance
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
                    return _instance = new AutoLoginService();
                }
            }
        }
        #endregion

        private User _user;

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///  Represents user autologin if user didn't log out previously before closing the app
        /// </summary>
        public AutoLoginService()
        {
            DeserializeLastUser();
        }


        /// <summary>
        ///  Deserialize information about the last logged in user; get information about user with such an id from server;
        ///  store user info in <c>CurrentUser</c> property 
        /// </summary>
        private void DeserializeLastUser()
        {
            User userCandidate;
            try
            {
                userCandidate = Serializator.Deserialize<User>(FileFolderHelper.LastUserFilePath);
            }
            catch (Exception ex)
            {
                userCandidate = null;
                Logger.Log("Failed to Deserialize last user", ex);
            }
            if (userCandidate == null)
            {
                Logger.Log("User was not deserialized");
                return;
            }
            using (var restClient = new UserApiService())
            {
                userCandidate = restClient.GetUserByGuid(userCandidate.Guid);
            }
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
        }

        /// <summary>
        /// Serialize information about the current user (stored in <c>CurrentUser</c> property)
        /// </summary>
        private void SerializeLastUser()
        {
            try
            {
                Serializator.SerializeOrDeleteFile<User>(CurrentUser, FileFolderHelper.LastUserFilePath);
                Logger.Log("User was successfully serialized");
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to Serialize last user", ex);
            }
        }

        public void CloseApp()
        {
            SerializeLastUser();
            Environment.Exit(1);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
