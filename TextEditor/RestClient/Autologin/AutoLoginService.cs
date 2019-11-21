using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.RestClient.Autologin
{
    public class AutoLoginService
    {
        public static User CurrentUser { get; set; }

        static AutoLoginService()
        {
            DeserializeLastUser();
        }
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
            using (var restClient = new RestClientApiService())
            {
                userCandidate = restClient.GetUserByGuid(userCandidate.Guid);
            }
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
        }

        private static void SerializeLastUser()
        {
            try
            {
                Serializator.Serialize<User>(CurrentUser, FileFolderHelper.LastUserFilePath);
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
