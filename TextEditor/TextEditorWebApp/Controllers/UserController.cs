using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerImp;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TextEditorWebApp.Controllers
{
    public class UserController : ApiController, IUserService
    {
        IUserService userService = new UserServiceImpl();

        private IUserService UserService { get => userService; set => userService = value; }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return UserService.GetAllUsers();
        }

        [HttpGet]
        public User GetUserByGuid(Guid guid)
        {
            return UserService.GetUserByGuid(guid);
        }

        [HttpPost]
        public void AddUser([FromBody]User user)
        {
            UserService.AddUser(user);
        }

        [HttpGet]
        public bool UserExists(string loginOrEmail)
        {
            return UserService.UserExists(loginOrEmail);
        }

        [HttpGet]
        public User GetUserByLoginOrEmail(string loginOrEmail)
        {
            return UserService.GetUserByLoginOrEmail(loginOrEmail);
        }
    }
}
