﻿using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerInterface
{
    public interface IUserRequestService
    {
        [OperationContract]
        IEnumerable<UserRequest> GetUserRequests(int userId);

        [OperationContract]
        void addUserRequest(int userId, UserRequest request);
    }
}
