using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(10000);
            using (var restClient = new RestClientApiService())
            {
                var user = new User("test", "test1", "test@gmail.com", "123");
                restClient.AddUser(user);
                Console.WriteLine("User list");
                IEnumerable<User> users = restClient.GetAllUsers();
                foreach (User u in users)
                    Console.WriteLine(u.FirstName+" "+u.LastName+" "+u.Email);
            }
            Console.ReadKey();
        }
    }
}
