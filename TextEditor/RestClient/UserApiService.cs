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

        #region Requests to server

        /// <summary>
        /// Sends post request to server to save information about new user
        /// </summary>
        /// <param name="user">User to save</param>
        public void AddUser(User user)
        {
            var response = _client.PostAsJsonAsync(_serviceBaseUri.AddSegment(nameof(AddUser)), user).Result;

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Create failed with " + response.StatusCode.ToString());
        }

        /// <summary>
        /// Sends get request to server to get all users
        /// </summary>
        /// <returns>List of all users</returns>
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

        /// <summary>
        /// Sends get request to server to check if user with the specified nickname or email exists
        /// </summary>
        /// <param name="loginOrEmail">User's nickname or email</param>
        /// <returns><c>true</c> if user with specified nickname or email exists, otherwise <c>false</c></returns>
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

        /// <summary>
        /// Sends get request to server to get user info by their id
        /// </summary>
        /// <param name="guid">User's id</param>
        /// <returns>Information about the user</returns>
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

        /// <summary>
        /// Sends get request to server to get user info by their nickname or email
        /// </summary>
        /// <param name="loginOrEmail">User's login or email</param>
        /// <returns>Information about the user</returns>
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
    #endregion

    #region Extention methods for Uri class
    static class UriExtensions
    {
        /// <summary>
        /// Adds a segment to the specified Uri by creating a new Uri with added segment
        /// </summary>
        /// <remarks>The returned Uri has this format: "old_uri/segment" </remarks>
        /// <param name="originalUri">Uri to which segment is added</param>
        /// <param name="segment">Segment to add</param>
        /// <returns>Uri with added segment</returns>
        public static Uri AddSegment(this Uri originalUri, string segment)
        {
            UriBuilder ub = new UriBuilder(originalUri);
            ub.Path = ub.Path + ((ub.Path.EndsWith("/")) ? "" : "/") + segment;

            return ub.Uri;
        }

        /// <summary>
        /// Ads parameter to the specified Uri by creating a new Uri with added segment
        /// </summary>
        /// <remarks>The returned Uri has this format: <c>old_uri?paramName=paramValue</c> 
        /// or <c>old_uri&paramName=paramValue</c> if it already contains some parameters</remarks>
        /// <param name="originalUri">Uri to which parameter is added</param>
        /// <param name="paramName">Name of the parameter to add</param>
        /// <param name="paramValue">Value of the added parameter</param>
        /// <returns></returns>
        public static Uri AddUriParam(this Uri originalUri, string paramName, string paramValue)
        {
            UriBuilder ub = new UriBuilder(originalUri);
            var query = HttpUtility.ParseQueryString(ub.Query);
            query[paramName] = paramValue;
            ub.Query = query.ToString();
            return ub.Uri;
        }
    }
    #endregion
}
