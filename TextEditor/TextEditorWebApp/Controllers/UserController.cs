using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerImp;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace TextEditorWebApp.Controllers
{
    public class UserController : ApiController, IUserService
    {
        IUserService userService = new UserServiceImpl();

        private IUserService UserService { get => userService; set => userService = value; }

        /// <summary>
        /// HTTP GET to retrieve all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return UserService.GetAllUsers();
        }

        /// <summary>
        /// HTTP GET to retrieve information about user by their id
        /// </summary>
        /// <param name="guid">Id of the user</param>
        /// <returns>Information about the certain user</returns>
        [HttpGet]
        public User GetUserByGuid(Guid guid)
        {
            return UserService.GetUserByGuid(guid);
        }

        /// <summary>
        /// HTTP POST to save information about a new user
        /// </summary>
        /// <param name="user">Information about the new user</param>
        [HttpPost]
        public void AddUser([FromBody]User user)
        {
            UserService.AddUser(user);
        }

        /// <summary>
        /// HTTP GET to check whether user with specified nickname or email already exists
        /// </summary>
        /// <param name="loginOrEmail">User's nickname or email</param>
        /// <returns><c>true</c> if user with specified nickname or email exists, otherwise <c>false</c></returns>
        [HttpGet]
        public bool UserExists(string loginOrEmail)
        {
            return UserService.UserExists(loginOrEmail);
        }

        /// <summary>
        /// HTTP GET to retrive information about the user bu their nickname or email
        /// </summary>
        /// <param name="loginOrEmail">User's nickname or email</param>
        /// <returns>Information about the certain user</returns>
        [HttpGet]
        public User GetUserByLoginOrEmail(string loginOrEmail)
        {
            return UserService.GetUserByLoginOrEmail(loginOrEmail);
        }
    }
}
