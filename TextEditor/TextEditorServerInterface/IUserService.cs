using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerInterface
{
    public interface IUserService
    {
        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        User GetUserByGuid(Guid guid);

        [OperationContract]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        bool UserExists(string loginOrEmail);

        [OperationContract]
        User GetUserByLoginOrEmail(string loginOrEmail);
    }
}
