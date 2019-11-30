using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;

namespace KMA.APRZP2019.TextEditorProject.RestClient.Autologin
{
    /// <summary>
    /// Automatically logins user from file
    /// </summary>
    public static class AutoLoginService
    {
        public static User CurrentUser { get; set; }

        static AutoLoginService()
        {
            DeserializeLastUser();
        }

        /// <summary>
        ///  Deserialize information about the last logged in user; get information about user with such an id from server;
        ///  store user info in <c>CurrentUser</c> property 
        /// </summary>
        private static void DeserializeLastUser()
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
        private static void SerializeLastUser()
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

        public static void CloseApp()
        {
            SerializeLastUser();
            Environment.Exit(1);
        }
    }
}
