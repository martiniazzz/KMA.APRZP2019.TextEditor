using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerInterface
{
    public interface IUserRequestService
    {
        [OperationContract]
        IEnumerable<UserRequest> GetUserRequests(Guid userGuid);

        [OperationContract]
        void AddUserRequest(Guid userGuid, UserRequest request);
    }
}
