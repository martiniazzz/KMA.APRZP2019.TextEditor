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
        /// <summary>
        /// Saves new user in database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="user">Information about new user to save</param>
        public void AddUser(User user)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets user by id from database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="guid">Id of the user</param>
        /// <returns>Information about the retrived user</returns>
        public User GetUserByGuid(Guid guid)
        {
            using (TextEditorDbContext context = new TextEditorDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Guid == guid);
            }
        }

        /// <summary>
        /// Gets list of all users from database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <returns>List of all users</returns>
        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.ToList();
            }
        }

        /// <summary>
        /// Checks whether user with specified nickname or email exists in database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="loginOrEmail">Nickame or email of the user</param>
        /// <returns><c>true</c> if user with specified nickname or email exists, otherwise <c>false</c></returns>
        public bool UserExists(string loginOrEmail)
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.Any(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            }
        }

        /// <summary>
        /// Gets user by their nickname or email from database
        /// </summary>
        /// <remarks>using Entity Framework</remarks>
        /// <param name="loginOrEmail">Nickame or email of the user</param>
        /// <returns> Information about retrived user</returns>
        public User GetUserByLoginOrEmail(string loginOrEmail)
        {
            using (var context = new TextEditorDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            }
        }
    }
}
