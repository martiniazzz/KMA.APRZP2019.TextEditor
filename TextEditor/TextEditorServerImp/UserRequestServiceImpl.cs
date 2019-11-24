using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerImp
{
    public class UserRequestServiceImpl : IUserRequestService
    {
        public void AddUserRequest(Guid userGuid, UserRequest request)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                context.Users.Single(x => x.Guid == userGuid).Requests.Add(request);
                context.SaveChanges();
            }
        }

        public IEnumerable<UserRequest> GetUserRequests(Guid userGuid)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                return context.Users.Include(u => u.Requests).Single(x => x.Guid == userGuid).Requests.OrderByDescending(r => r.ChangedAt).ToList();
            }
        }

    }
}
