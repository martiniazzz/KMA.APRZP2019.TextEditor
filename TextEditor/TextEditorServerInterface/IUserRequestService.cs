using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerInterface
{
    public interface IUserRequestService
    {
        /// <summary>
        /// Gets all requests for certain user
        /// </summary>
        /// <param name="userGuid">User id to retrieve information for</param>
        /// <returns>List of user's requests</returns>
        [OperationContract]
        IEnumerable<UserRequest> GetUserRequests(Guid userGuid);

        /// <summary>
        /// Saves new user request
        /// </summary>
        /// <param name="userGuid">Id of user who created request</param>
        /// <param name="request">Information about new request</param>
        [OperationContract]
        void AddUserRequest(Guid userGuid, UserRequest request);
    }
}
