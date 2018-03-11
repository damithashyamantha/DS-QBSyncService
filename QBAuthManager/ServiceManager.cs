// Description  ServiceManager
// Namespace    QBAuthManager
// Author       Damitha Shyamantha      Date    12/04/2017

#region UsingDirectives
using QBAuthManager.Helpers;
using QBAuthManager.Models;
using QBEntity.System;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
#endregion

namespace QBAuthManager
{
    /// <summary>
    /// service manager implementation
    /// </summary>
    /// <seealso cref="QBAuthManager.BaseManager" />
    public class ServiceManager : BaseManager
    {
        #region PrivateMembers
        /// <summary>
        /// The settings
        /// </summary>
        private static ServiceSettings _settings;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ServiceManager(ServiceSettings settings)
        {
            _settings = settings;
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Updates the customer asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Data is not provided
        /// or
        /// Error in UpdateCustomer
        /// </exception>
        public async Task<QBResponce> UpdateCustomerAsync(byte[] data)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("Data is not provided");

            try
            {
                return await UpdateEntityAsync(data, Constants.CustomerSuffix);
            }
            // catch web error
            catch (WebException webException)
            {
                return await GetErrorResponseAsync(webException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error in UpdateCustomer", exception);
            }
        }

        /// <summary>
        /// Creates the entity asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="qbType">Type of the qb.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// data
        /// or
        /// qbType
        /// </exception>
        /// <exception cref="System.Exception">Error in creating entity</exception>
        public async Task<QBResponce> CreateEntityAsync(byte[] data, string qbType)
        {
            if (data.Length <= 0)
                throw new ArgumentNullException("data");

            if (string.IsNullOrEmpty(qbType))
                throw new ArgumentNullException("qbType");

            try
            {
                string uri = Builder.BuildPostUri(_settings.BaseUrl, _settings.Token.RealmId, qbType);
                var request = Builder.BuildRequestRequest(_settings.Token.AccessToken, uri, Constants.HttpPostReques);

                return await PostRequestAsync(request, data);


            }
            catch (WebException webException)
            {
                return await GetErrorResponseAsync(webException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error in creating entity", exception);
            }
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="qbType">Type of the qb.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// data
        /// or
        /// qbType
        /// </exception>
        /// <exception cref="System.Exception">Error in creating entity</exception>
        public QBResponce CreateEntity(byte[] data, string qbType)
        {
            if (data.Length <= 0)
                throw new ArgumentNullException("data");

            if (string.IsNullOrEmpty(qbType))
                throw new ArgumentNullException("qbType");

            try
            {
                string uri = Builder.BuildPostUri(_settings.BaseUrl, _settings.Token.RealmId, qbType);
                var request = Builder.BuildRequestRequest(_settings.Token.AccessToken, uri, Constants.HttpPostReques);

                return PostRequest(request, data);
            }
            catch (WebException webException)
            {
                return GetErrorResponse(webException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error in creating entity", exception);
            }
        }

        /// <summary>
        /// Sets the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="System.ArgumentNullException">token</exception>
        public void SetToken(TokenBaerer token)
        {
            if (token == null)
                throw new ArgumentNullException("token");
            _settings.Token = token;
        }

        /// <summary>
        /// Updates the entity asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="qbType">Type of the qb.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// data
        /// or
        /// qbType
        /// </exception>
        /// <exception cref="System.Exception">Error in updateing entity</exception>
        public async Task<QBResponce> UpdateEntityAsync(byte[] data, string qbType)
        {
            if (data.Length <= 0)
                throw new ArgumentNullException("data");

            if (string.IsNullOrEmpty(qbType))
                throw new ArgumentNullException("qbType");
            try
            {
                string uri = Builder.BuildUpdateUri(_settings.BaseUrl, _settings.Token.RealmId, qbType);
                var request = Builder.BuildRequestRequest(_settings.Token.AccessToken, uri, Constants.HttpPostReques);
                return await PostRequestAsync(request, data);
            }
            catch (WebException webException)
            {
                return await GetErrorResponseAsync(webException);
            }
            catch (Exception exception)
            {
                throw new Exception("Error in updateing entity", exception);
            }
        }

        /// <summary>
        /// Gets the entity asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        public async Task<HttpWebResponse> GetEntityAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException("query");

            string uri = Builder.BuildQueryUri(_settings.BaseUrl, _settings.Token.RealmId, query);

            HttpWebRequest getRequest = Builder.BuildRequestRequest(_settings.Token.AccessToken, uri, Constants.HttpGetReques);

            WebResponse getResponse = await getRequest.GetResponseAsync();

            return (HttpWebResponse)getResponse;
        }

        /// <summary>
        /// Gets the entity by identifier asynchronous.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// target
        /// or
        /// id
        /// </exception>
        public async Task<HttpWebResponse> GetEntityByIdAsync(string target, int id)
        {
            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");
            if (id <= 0)
                throw new ArgumentNullException("id");
            try
            {
                string uri = Builder.BuildGetUri(_settings.BaseUrl, _settings.Token.RealmId, target, id);
                HttpWebRequest getRequest = Builder.BuildRequestRequest(_settings.Token.AccessToken, uri, Constants.HttpGetReques);
                WebResponse getResponse = await getRequest.GetResponseAsync();
                return (HttpWebResponse)getResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region PrivateMethods                        
        /// <summary>
        /// Posts the request asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private async Task<QBResponce> PostRequestAsync(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;

            using (var stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }

            WebResponse webResponse = await request.GetResponseAsync();
            using (HttpWebResponse response = (HttpWebResponse)webResponse)
            {
                return await GetResopnceAsync(response);
            }
        }

        private QBResponce PostRequest(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                 stream.Write(data, 0, data.Length);
            }

            WebResponse webResponse =  request.GetResponse();
            using (HttpWebResponse response = (HttpWebResponse)webResponse)
            {
                return  GetResopnce(response);
            }
        }

        /// <summary>
        /// Reads the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        private async Task<string> ReadResponse(HttpWebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Gets the web error response.
        /// </summary>
        /// <param name="webException">The web exception.</param>
        /// <returns></returns>
        private async Task<QBResponce> GetErrorResponseAsync(WebException webException)
        {
            using (WebResponse responce = webException.Response)
            {
                return await GetResopnceAsync((HttpWebResponse)responce);
            }
        }

        private QBResponce GetErrorResponse(WebException webException)
        {
            using (WebResponse responce = webException.Response)
            {
                return  GetResopnce((HttpWebResponse)responce);
            }
        }


        private async Task<QBResponce> GetResopnceAsync(HttpWebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string responceString = await reader.ReadToEndAsync();
                return new QBResponce { StatusCode = response.StatusCode, Reponce = responceString };
            }

        }

        private QBResponce GetResopnce(HttpWebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string responceString =  reader.ReadToEnd();
                return new QBResponce { StatusCode = response.StatusCode, Reponce = responceString };
            }

        }
        #endregion
    }
}
