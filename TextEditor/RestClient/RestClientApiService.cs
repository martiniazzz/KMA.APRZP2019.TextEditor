using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    class RestClientApiService : IDisposable, IUserService
    {

        HttpClient _client = new HttpClient();
        Uri _serviceBaseUri;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        public RestClientApiService()
        {
            _serviceBaseUri = new Uri(@"https://localhost:44341/api/User");
        }
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

        ~RestClientApiService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void AddUser(User user)
        {
            var response = _client.PostAsJsonAsync(_serviceBaseUri.AddSegment(nameof(AddUser)), user).Result;
            Console.WriteLine(response.ToString());

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
            else if (response.StatusCode != HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException("Get failed with " + response.StatusCode.ToString());
            }

            return new List<User>();
        }
        #endregion

    }

    static class UriExtensions
    {
        public static Uri AddSegment(this Uri originalUri, string segment)
        {
            UriBuilder ub = new UriBuilder(originalUri);
            ub.Path = ub.Path + ((ub.Path.EndsWith("/")) ? "" : "/") + segment;

            return ub.Uri;
        }
    }
}
