// Description  Builder
// Namespace    QBAuthManager.Helpers
// Author       Damitha Shyamantha      Date    12/11/2017

#region UsingDirecives
using System;
using System.Net;
#endregion

namespace QBAuthManager.Helpers
{
    /// <summary>
    /// builder class
    /// </summary>
    public static class Builder
    {
        #region PublicMethods
        /// <summary>
        /// Builds the URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// baseUri
        /// or
        /// realmId
        /// or
        /// query
        /// </exception>
        /// <exception cref="System.Exception">Error in UriBuilder</exception>
        public static string BuildQueryUri(string baseUri, string realmId, string query)
        {
            try
            {
                if (string.IsNullOrEmpty(baseUri))
                    throw new ArgumentNullException("baseUri");

                if (string.IsNullOrEmpty(realmId))
                    throw new ArgumentNullException("realmId");

                if (string.IsNullOrEmpty(query))
                    throw new ArgumentNullException("query");
                string encodedQuery = WebUtility.UrlEncode(query);

                return string.Format("{0}v3/company/{1}/query?query={2}", baseUri, realmId, encodedQuery);
            }
            catch (Exception exception)
            {

                throw new Exception("Error in UriBuilder", exception);
            }
        }

        /// <summary>
        /// Builds the post URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// baseUri
        /// or
        /// realmId
        /// or
        /// target
        /// </exception>
        /// <exception cref="System.Exception">BuildPostUri</exception>
        public static string BuildPostUri(string baseUri, string realmId, string target)
        {
            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentNullException("baseUri");

            if (string.IsNullOrEmpty(realmId))
                throw new ArgumentNullException("realmId");

            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");
            try
            {
                return string.Format("{0}v3/company/{1}/{2}", baseUri, realmId, target);
            }
            catch (Exception exception)
            {
                throw new Exception("BuildPostUri", exception);
            }
        }

        /// <summary>
        /// Builds the update URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// baseUri
        /// or
        /// realmId
        /// or
        /// target
        /// </exception>
        /// <exception cref="System.Exception">BuildUpdateUri</exception>
        public static string BuildUpdateUri(string baseUri, string realmId, string target)
        {
            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentNullException("baseUri");

            if (string.IsNullOrEmpty(realmId))
                throw new ArgumentNullException("realmId");

            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");
            try
            {
                return string.Format("{0}v3/company/{1}/{2}?operation=update&minorversion=4", baseUri, realmId, target);
            }
            catch (Exception exception)
            {

                throw new Exception("BuildUpdateUri", exception);
            }
        }

        /// <summary>
        /// Builds the get URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <param name="target">The target.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// baseUri
        /// or
        /// realmId
        /// or
        /// target
        /// or
        /// id
        /// </exception>
        /// <exception cref="System.Exception">BuildUpdateUri</exception>
        public static string BuildGetUri(string baseUri, string realmId, string target, int id)
        {
            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentNullException("baseUri");

            if (string.IsNullOrEmpty(realmId))
                throw new ArgumentNullException("realmId");

            if (string.IsNullOrEmpty(target))
                throw new ArgumentNullException("target");
            if (id <= 0)
                throw new ArgumentNullException("id");
            try
            {
                return string.Format("{0}v3/company/{1}/{2}/{3}", baseUri, realmId, target,id);
            }
            catch (Exception exception)
            {

                throw new Exception("BuildUpdateUri", exception);
            }
        }

        /// <summary>
        /// Builds the request request.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="requestMethod">The request method.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error in BuildGetRequest</exception>
        public static HttpWebRequest BuildRequestRequest(string accessToken, string uri, string requestMethod)
        {
            try
            {
                HttpWebRequest qboApiRequest = (HttpWebRequest)WebRequest.Create(uri);
                qboApiRequest.Method = requestMethod;
                qboApiRequest.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
                qboApiRequest.ContentType = Constants.HTTPJsonUtf8;
                qboApiRequest.Accept = Constants.HttpJsonContent;

                return qboApiRequest;
            }
            catch (Exception exception)
            {
                throw new Exception("Error in BuildGetRequest", exception);
            }
        }
        #endregion

    }
}