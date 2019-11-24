using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.TextEditorServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.RestClient
{
    public class UserRequestApiService: IDisposable, IUserRequestService
    {

        private HttpClient _client = new HttpClient();
        private Uri _serviceBaseUri;

        public UserRequestApiService()
        {
            _serviceBaseUri = new Uri(@"https://localhost:44341/api/UserRequest");
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

        ~UserRequestApiService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
             GC.SuppressFinalize(this);
        }
        #endregion

        public IEnumerable<UserRequest> GetUserRequests(Guid userGuid)
        {
            var response = _client.GetAsync(_serviceBaseUri.AddSegment(nameof(GetUserRequests)).AddUriParam(nameof(userGuid), userGuid.ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<UserRequest>>().Result;
            }
            else
            {
                throw new InvalidOperationException("Get user requests failed with " + response.StatusCode.ToString());
            }
        }

        public void AddUserRequest(Guid userGuid, UserRequest request)
        {
            var response = _client.PostAsJsonAsync(_serviceBaseUri.AddSegment(nameof(AddUserRequest)).AddUriParam(nameof(userGuid), userGuid.ToString()),request).Result;

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Create user request failed with " + response.StatusCode.ToString());
        }
  
    }
}
