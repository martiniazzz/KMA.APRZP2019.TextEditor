using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerInterface
{
    public interface IUserService
    {
        /// <summary>
        /// Save information about new user
        /// </summary>
        /// <param name="user">Information about new user to save</param>
        [OperationContract]
        void AddUser(User user);

        /// <summary>
        ///  Gets information about user by their id
        /// </summary>
        /// <param name="guid">Id of te user</param>
        /// <returns>Information about the retrived user</returns>
        [OperationContract]
        User GetUserByGuid(Guid guid);

        /// <summary>
        ///  Gets list of all users
        /// </summary>
        /// <returns>List of all users</returns>
        [OperationContract]
        IEnumerable<User> GetAllUsers();

        /// <summary>
        ///  Checks whether user with specified nickname or email exists
        /// </summary>
        /// <param name="loginOrEmail">Nickame or email of the user</param>
        /// <returns><c>true</c> if user with specified nickname or email exists, otherwise <c>false</c></returns>
        [OperationContract]
        bool UserExists(string loginOrEmail);

        /// <summary>
        /// Gets user by their nickname or email
        /// </summary>
        /// <param name="loginOrEmail">>Nickame or email of the user</param>
        /// <returns>Information about retrived user</returns>
        [OperationContract]
        User GetUserByLoginOrEmail(string loginOrEmail);
    }
}
