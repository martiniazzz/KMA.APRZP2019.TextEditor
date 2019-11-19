using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.TextEditorServerImp
{
    public class UserServiceImpl : IUserService
    {
        public void AddUser(User user)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
