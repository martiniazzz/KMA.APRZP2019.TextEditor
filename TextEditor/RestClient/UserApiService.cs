using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KMA.APRZP2019.TextEditorProject.RestClient
{
    public class UserApiService : IDisposable, IUserService
    {

        private HttpClient _client = new HttpClient();
        private Uri _serviceBaseUri;

        public UserApiService()
        {
            _serviceBaseUri = new Uri(@"https://localhost:44341/api/User");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client = null;
                    _serviceBaseUri = null;
                }

                disposedValue = true;
            }
        }

        ~UserApiService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void AddUser(User user)
        {
            var response = _client.PostAsJsonAsync(_serviceBaseUri.AddSegment(nameof(AddUser)), user).Result;

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Create failed with " + response.StatusCode.ToString());
        }

        public IEnumerable<User> GetAllUsers()
        {
            var response = _client.GetAsync(_serviceBaseUri.AddSegment(nameof(GetAllUsers))).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<User>>().Result;

            }
            else
            {
                throw new InvalidOperationException("Get failed with " + response.StatusCode.ToString());
            }
        }

        public bool UserExists(string loginOrEmail)
        {
            var response = _client.GetAsync(_serviceBaseUri.AddSegment(nameof(UserExists)).AddUriParam(nameof(loginOrEmail), loginOrEmail)).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<bool>().Result;
            }
            else
            {
                throw new InvalidOperationException("Get failed with " + response.StatusCode.ToString());
            }
        }

        public User GetUserByGuid(Guid guid)
        {
            var response = _client.GetAsync(_serviceBaseUri.AddSegment(nameof(GetUserByGuid)).AddUriParam(nameof(guid), guid.ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            else
            {
                throw new InvalidOperationException("Get failed with " + response.StatusCode.ToString());
            }
        }

        public User GetUserByLoginOrEmail(string loginOrEmail)
        {
            var response = _client.GetAsync(_serviceBaseUri.AddSegment(nameof(GetUserByLoginOrEmail)).AddUriParam(nameof(loginOrEmail), loginOrEmail)).Result;
            Console.WriteLine(_serviceBaseUri.AddSegment(nameof(GetUserByLoginOrEmail)).AddUriParam(nameof(loginOrEmail), loginOrEmail));
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            else
            {
                throw new InvalidOperationException("Get failed with " + response.StatusCode.ToString());
            }
        }
    }

    static class UriExtensions
    {
        public static Uri AddSegment(this Uri originalUri, string segment)
        {
            UriBuilder ub = new UriBuilder(originalUri);
            ub.Path = ub.Path + ((ub.Path.EndsWith("/")) ? "" : "/") + segment;

            return ub.Uri;
        }

        public static Uri AddUriParam(this Uri originalUri, string paramName, string paramValue)
        {
            UriBuilder ub = new UriBuilder(originalUri);
            var query = HttpUtility.ParseQueryString(ub.Query);
            query[paramName] = paramValue;
            ub.Query = query.ToString();
            return ub.Uri;
        }
    }
}
