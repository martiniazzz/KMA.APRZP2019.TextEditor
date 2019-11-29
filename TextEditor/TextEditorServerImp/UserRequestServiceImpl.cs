using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerImp
{
    public class UserRequestServiceImpl : IUserRequestService
    {
        /// <summary>
        /// Saves new user request about changing file in the database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="userGuid">Id of the user who created request</param>
        /// <param name="request">New user request to save</param>
        public void AddUserRequest(Guid userGuid, UserRequest request)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                context.Users.Single(x => x.Guid == userGuid).Requests.Add(request);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all request about changes in files from database for certain user
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="userGuid">Id of the user to retrieve requests</param>
        /// <returns>List of user requests</returns>
        public IEnumerable<UserRequest> GetUserRequests(Guid userGuid)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                return context.Users.Include(u => u.Requests).Single(x => x.Guid == userGuid).Requests.OrderByDescending(r => r.ChangedAt).ToList();
            }
        }

    }
}
