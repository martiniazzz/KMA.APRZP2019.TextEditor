using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerImp;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMA.APRZP2019.TextEditorProject.TextEditorWebApp.Controllers
{
    public class UserRequestController : ApiController, IUserRequestService
    {
        private IUserRequestService _userRequestSerice = new UserRequestServiceImpl();

        private IUserRequestService UserRequestService { get => _userRequestSerice; set => _userRequestSerice = value; }

        [HttpPost]
        public void AddUserRequest(Guid userGuid, [FromBody]UserRequest request)
        {
            UserRequestService.AddUserRequest(userGuid, request);
        }

        [HttpGet]
        public IEnumerable<UserRequest> GetUserRequests(Guid userGuid)
        {
            return UserRequestService.GetUserRequests(userGuid);
        }
    }
}
