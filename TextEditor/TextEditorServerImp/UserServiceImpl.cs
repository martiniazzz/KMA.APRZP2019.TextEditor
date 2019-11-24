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

        public User GetUserByGuid(Guid guid)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Guid == guid);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.ToList();
            }
        }

        public bool UserExists(string loginOrEmail)
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.Any(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            }
        }

        public User GetUserByLoginOrEmail(string loginOrEmail)
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            }
        }
    }
}
