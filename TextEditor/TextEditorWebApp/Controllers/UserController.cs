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
    public class UserController : ApiController
    {
        IUserService userService = new UserServiceImpl();

        private IUserService UserService { get => userService; set => userService = value; }

        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}

        [HttpGet]
        public IEnumerable<User> getUsers()
        {
            return UserService.GetAllUsers();
        }

        [HttpPost]
        public void AddUser([FromBody]User user)
        {
            UserService.AddUser(user);
        }
    }
}
