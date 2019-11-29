using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerImp;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace KMA.APRZP2019.TextEditorProject.TextEditorWebApp.Controllers
{
    public class UserRequestController : ApiController, IUserRequestService
    {
        private IUserRequestService _userRequestSerice = new UserRequestServiceImpl();

        private IUserRequestService UserRequestService { get => _userRequestSerice; set => _userRequestSerice = value; }

        /// <summary>
        /// HTTP POST to save information about new user request on changing files
        /// </summary>
        /// <param name="userGuid">Id of the user who created request</param>
        /// <param name="request">Information about new user's request</param>
        [HttpPost]
        public void AddUserRequest(Guid userGuid, [FromBody]UserRequest request)
        {
            UserRequestService.AddUserRequest(userGuid, request);
        }

        /// <summary>
        /// HTTP GET to retrive list of requests of certain user
        /// </summary>
        /// <param name="userGuid">User's id</param>
        /// <returns>List of user's requests</returns>
        [HttpGet]
        public IEnumerable<UserRequest> GetUserRequests(Guid userGuid)
        {
            return UserRequestService.GetUserRequests(userGuid);
        }
    }
}
