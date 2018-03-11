// Description  AuthManager
// Namespace    QBAuthManager
// Author       Damitha Shyamantha      Date    12/04/2017

#region UsingDirectives
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using QBAuthManager.Helpers;
using QBAuthManager.Interfaces;
using QBAuthManager.Models;
using QBEntity.QB;
using QBEntity.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace QBAuthManager
{
    /// <summary>
    /// Auth manager implementataion
    /// </summary>
    /// <seealso cref="QBAuthManager.Interfaces.IAuthManager" />
    /// <seealso cref="System.IDisposable" />
    public class AuthManager : BaseManager, IAuthManager
    {
        #region PublicProperties
        // OAuth2 client configuration        
        /// <summary>
        /// Gets the redirect URI.
        /// </summary>
        public string RedirectUri { get; private set; }

        /// <summary>
        /// Gets the discovery URL.
        /// </summary>
        public string DiscoveryUrl { get; private set; }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the client secret.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// OidcScopes.Accounting.GetStringValue();
        public string Scope { get; private set; }

        /// <summary>
        /// Gets the service context base URL.
        /// </summary>
        public string ServiceContextBaseUrl { get; private set; }

        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        public string LogPath { get; set; }
        #endregion

        #region PublicMembers
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        #endregion

        #region PrivateMembers
        private static string authorizationEndpoint;
        private static string tokenEndpoint;
        private static string userinfoEndPoint;
        private static string revokeEndpoint;
        private static string issuerUrl;
        private static string jwksEndpoint;
        private static string stateVal;
        private static string mod;
        private static string expo;
        private static readonly RandomNumberGenerator randomNumGenerator = RandomNumberGenerator.Create();

        private static string cred;
        private static string enc;
        private static string basicAuth;

        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthManager"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="System.ArgumentNullException">
        /// RedirectUrl
        /// or
        /// DiscoveryUrl
        /// or
        /// ClientId
        /// or
        /// ClientSecret
        /// or
        /// Scope
        /// or
        /// ServiceContextBaseUrl
        /// </exception>
        public AuthManager(OAuthSettings settings)
        {
            if (string.IsNullOrEmpty(settings.RedirectUri))
                throw new ArgumentNullException("RedirectUrl");
            else
                RedirectUri = settings.RedirectUri;

            if (string.IsNullOrEmpty(settings.DiscoveryUrl))
                throw new ArgumentNullException("DiscoveryUrl");
            else
                DiscoveryUrl = settings.DiscoveryUrl;

            if (string.IsNullOrEmpty(settings.ClientId))
                throw new ArgumentNullException("ClientId");
            else
                ClientId = settings.ClientId;

            if (string.IsNullOrEmpty(settings.ClientSecret))
                throw new ArgumentNullException("ClientSecret");
            else
                ClientSecret = settings.ClientSecret;

            if (string.IsNullOrEmpty(settings.Scope))
                throw new ArgumentNullException("Scope");
            else
                Scope = settings.Scope;

            if (string.IsNullOrEmpty(settings.ServiceContextBaseUrl))
                throw new ArgumentNullException("ServiceContextBaseUrl");
            else
                ServiceContextBaseUrl = settings.ServiceContextBaseUrl;

            cred = string.Format("{0}:{1}", ClientId, ClientSecret);
            enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(cred));
            basicAuth = string.Format("{0} {1}", "Basic", enc);
        }
        #endregion

        #region PublicMethods                        
        /// <summary>
        /// Gets the discovery data asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">GetDiscoveryData</exception>
        public async Task<DiscoveryData> GetDiscoveryDataAsync()
        {
            try
            {
                //call Discovery endpoint
                HttpWebRequest discoveryRequest = BuildDiscoveryRequest();
                WebResponse discoveryResponce = await discoveryRequest.GetResponseAsync();

                using (HttpWebResponse response = (HttpWebResponse)discoveryResponce)
                {
                    using (var tokenReader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseText = await tokenReader.ReadToEndAsync();
                        DiscoveryData data = JsonConvert.DeserializeObject<DiscoveryData>(responseText);
                        if (data != null)
                        {
                            //Authorization endpoint url
                            authorizationEndpoint = data.AuthorizationEndpoint;

                            //Token endpoint url
                            tokenEndpoint = data.TokenEndpoint;

                            //UseInfo endpoint url
                            userinfoEndPoint = data.UserinfoEndpoint;

                            //Revoke endpoint url
                            revokeEndpoint = data.RevocationEndpoint;

                            //Issuer endpoint Url 
                            issuerUrl = data.Issuer;

                            //Json Web Key Store Url
                            jwksEndpoint = data.JWKSUri;
                            return data;
                        }
                        else
                            throw new Exception("GetDiscoveryData");
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">authorizationEndpoint</exception>
        public string GetAuthRequest()
        {
            try
            {
                stateVal = GetCSRFToken();
                if (string.IsNullOrEmpty(authorizationEndpoint))
                    throw new ArgumentNullException("authorizationEndpoint");
                string authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
                    authorizationEndpoint,
                    ClientId,
                    Scope,
                    Uri.EscapeDataString(RedirectUri),
                    stateVal);
                return authorizationRequest;
            }
            catch (Exception exception)
            {
                throw new Exception("OAuth token request error: ", exception);
            }
        }

        /// <summary>
        /// Gets the CSRF token.
        /// </summary>
        /// <returns></returns>
        public string GetCSRFToken()
        {
            string csrfKey = "CSRF";
            if (dictionary.ContainsKey(csrfKey))
            {
                return dictionary[csrfKey];
            }
            else
            {
                var bytes = new byte[32];
                randomNumGenerator.GetBytes(bytes);
                string stateValue = ByteArrayToString(bytes);
                dictionary.Add(csrfKey, stateValue);
                return stateValue;
            }
        }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// code
        /// or
        /// realmId
        /// </exception>
        /// <exception cref="System.Exception">
        /// OAuth get token error:
        /// </exception>
        public async Task<TokenBaerer> GetTokensAsync(string code, string realmId)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("code");
            if (string.IsNullOrEmpty(realmId))
                throw new ArgumentNullException("realmId");
            try
            {
                TokenBaerer tokenBaerer = new TokenBaerer();

                HttpWebRequest accesstokenRequest = await BuildAccessTokenRequestAsync(code);

                WebResponse accesstokenResponse = await accesstokenRequest.GetResponseAsync();

                using (HttpWebResponse response = (HttpWebResponse)accesstokenResponse)
                {
                    using (var tokenReader = new StreamReader(response.GetResponseStream()))
                    {
                        string responceText = await tokenReader.ReadToEndAsync();
                        Dictionary<string, string> accessTokenDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responceText);

                        if (accessTokenDecoded.ContainsKey(Constants.ErrorKey))
                            throw new Exception(string.Concat("error", accessTokenDecoded[Constants.ErrorKey]));

                        if (accessTokenDecoded.ContainsKey(Constants.IdTokenKey))
                            tokenBaerer.IdentityToken = accessTokenDecoded[Constants.IdTokenKey];

                        if (accessTokenDecoded.ContainsKey(Constants.AccessTokenKey))
                            tokenBaerer.AccessToken = accessTokenDecoded[Constants.AccessTokenKey];

                        if (accessTokenDecoded.ContainsKey(Constants.RefershTokenKey))
                            tokenBaerer.RefreshToken = accessTokenDecoded[Constants.RefershTokenKey];

                        // refresh token expired in 100 days
                        tokenBaerer.ExpiaryDate = DateTime.UtcNow.AddDays(90);

                        // updated date
                        tokenBaerer.UpdatedDate = DateTime.UtcNow;

                        // set realm
                        tokenBaerer.RealmId = realmId;
                    }
                }
                return tokenBaerer;

            }
            catch (Exception exception)
            {
                throw new Exception("OAuth get token error: ", exception);
            }
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">refreshToken</exception>
        /// <exception cref="System.Exception">
        /// OAuth referesh token error:
        /// </exception>
        public async Task<TokenBaerer> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException("refreshToken");
            try
            {
                HttpWebRequest refereshtokenRequest = await BuildRefreshTokenRequestAsync(refreshToken);

                WebResponse refreshTokenResopnse = await refereshtokenRequest.GetResponseAsync();

                using (HttpWebResponse response = (HttpWebResponse)refreshTokenResopnse)
                {
                    using (var tokenReader = new StreamReader(response.GetResponseStream()))
                    {
                        string responceText = await tokenReader.ReadToEndAsync();
                        return GetTokenFormResponce(responceText);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("OAuth referesh token error: ", exception);
            }
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">refreshToken</exception>
        /// <exception cref="System.Exception">OAuth referesh token error:</exception>
        public TokenBaerer RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException("refreshToken");
            try
            {
                HttpWebRequest refereshtokenRequest = BuildRefreshTokenRequestAsync(refreshToken).Result;

                WebResponse refreshTokenResopnse = refereshtokenRequest.GetResponse();

                using (HttpWebResponse response = (HttpWebResponse)refreshTokenResopnse)
                {
                    using (var tokenReader = new StreamReader(response.GetResponseStream()))
                    {
                        string responceText = tokenReader.ReadToEnd();
                        return GetTokenFormResponce(responceText);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("OAuth referesh token error: ", exception);
            }
        }

        /// <summary>
        /// Revokes the token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// accessToken
        /// or
        /// refreshToken
        /// </exception>
        /// <exception cref="System.Exception">OAuth revoke token error:</exception>
        public async Task<HttpStatusCode> RevokeTokenAsync(string accessToken, string refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException("accessToken");

            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException("refreshToken");
            try
            {
                HttpWebRequest revoketokenRequest = await BuildRevokeTokenRequestAsync(refreshToken);

                WebResponse revokeTokenResponse = await revoketokenRequest.GetResponseAsync();

                using (HttpWebResponse response = (HttpWebResponse)revokeTokenResponse)
                {
                    return response.StatusCode;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("OAuth revoke token error: ", exception);
            }
        }

        /// <summary>
        /// Determines whether [is identifier token valid] [the specified identifier token].
        /// </summary>
        /// <param name="idToken">The identifier token.</param>
        /// <returns>
        ///   <c>true</c> if [is identifier token valid] [the specified identifier token]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">idToken</exception>
        /// <exception cref="System.Exception">Error validating identity token:</exception>
        public bool IsValidIdToken(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
                throw new ArgumentNullException("idToken");
            try
            {
                string[] splitValues = idToken.Split('.');
                if (splitValues[0] != null)
                {

                    //decode header 
                    var headerJson = Encoding.UTF8.GetString(FromBase64Url(splitValues[0].ToString()));
                    IdTokenHeader headerData = JsonConvert.DeserializeObject<IdTokenHeader>(headerJson);

                    //Verify if the key id of the key used to sign the payload is not null
                    if (headerData.Kid == null)
                        return false;

                    //Verify if the hashing alg used to sign the payload is not null
                    if (headerData.Alg == null)
                        return false;
                }
                if (splitValues[1] != null)
                {
                    //decode payload
                    var payloadJson = Encoding.UTF8.GetString(FromBase64Url(splitValues[1].ToString()));

                    IdTokenPayload payloadData = JsonConvert.DeserializeObject<IdTokenPayload>(payloadJson);

                    //verify aud matches clientId
                    if (payloadData.Aud != null)
                    {
                        if (payloadData.Aud[0].ToString() != ClientId)
                            return false;
                    }
                    else
                        return false;

                    //verify authtime matches the time the ID token was authorized.                
                    if (payloadData.Auth_time == null)
                        return false;

                    //verify exp matches the time the ID token expires, represented in Unix time (integer seconds).                
                    if (payloadData.Exp != null)
                    {
                        ulong expiration = Convert.ToUInt64(payloadData.Exp);

                        TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
                        TimeSpan unixTicks = new TimeSpan(DateTime.UtcNow.Ticks) - epochTicks;
                        ulong unixTime = Convert.ToUInt64(unixTicks.Milliseconds);
                        //Verify the expiration time with what you expiry time have calculated and saved in your application
                        if ((expiration - unixTime) <= 0)
                            return false;
                    }
                    else
                        return false;

                    //Verify iat matches the time the ID token was issued, represented in Unix time (integer seconds).            
                    if (payloadData.Iat == null)
                        return false;

                    //verify iss matches the  issuer identifier for the issuer of the response.     
                    if (payloadData.Iss != null)
                    {
                        if (payloadData.Iss.ToString() != issuerUrl)
                            return false;
                    }
                    else
                        return false;

                    //verify sub. sub is an identifier for the user, unique among all Intuit accounts and never reused. 
                    //An Intuit account can have multiple emails at different points in time, but the sub value is never changed.
                    //Use sub within your application as the unique-identifier key for the user.
                    if (payloadData.Sub == null)
                        return false;
                }

                //verify Siganture matches the sigend concatenation of the encoded header and the encoded payload with the specified algorithm
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                //Read values of n and e from discovery document.
                rsa.ImportParameters(
                  new RSAParameters()
                  {
                      //Read values from discovery document
                      Modulus = FromBase64Url(mod),
                      Exponent = FromBase64Url(expo)
                  });

                //verify using RSA signature
                SHA256 sha256 = SHA256.Create();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(splitValues[0] + '.' + splitValues[1]));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                if (rsaDeformatter.VerifySignature(hash, FromBase64Url(splitValues[2])))
                    return true;
                else
                    return false;
            }
            catch (Exception exception)
            {
                throw new Exception("Error validating identity token: ", exception);
            }
        }
        #endregion

        #region PrivateMethods        
        /// <summary>
        /// Froms the base64 URL.
        /// </summary>
        /// <param name="base64Url">The base64 URL.</param>
        /// <returns></returns>
        private byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// Bytes the array to string.
        /// </summary>
        /// <param name="ba">The ba.</param>
        /// <returns></returns>
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        /// <summary>
        /// Builds the token endpoint request asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<HttpWebRequest> BuildTokenEndpointRequestAsync()
        {
            if (string.IsNullOrEmpty(tokenEndpoint))
                await GetDiscoveryDataAsync();

            HttpWebRequest accesstokenRequest = (HttpWebRequest)WebRequest.Create(tokenEndpoint);
            accesstokenRequest.Method = Constants.HttpPostReques;
            accesstokenRequest.ContentType = Constants.UrlEncodedContent;
            accesstokenRequest.Accept = Constants.HttpJsonContent;
            accesstokenRequest.Headers[HttpRequestHeader.Authorization] = basicAuth;//Adding Authorization header

            return accesstokenRequest;
        }

        /// <summary>
        /// Builds the revoke token endpoint request asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<HttpWebRequest> BuildRevokeTokenEndpointRequestAsync()
        {
            if (string.IsNullOrEmpty(revokeEndpoint))
                await GetDiscoveryDataAsync();

            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(revokeEndpoint);
            tokenRequest.Method = Constants.HttpPostReques;
            tokenRequest.ContentType = Constants.HttpJsonContent;
            tokenRequest.Accept = Constants.HttpJsonContent;
            //Add Authorization header
            tokenRequest.Headers[HttpRequestHeader.Authorization] = basicAuth;

            return tokenRequest;
        }

        /// <summary>
        /// Formats the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestBody">The request body.</param>
        private void FormatRequest(HttpWebRequest request, string requestBody)
        {
            byte[] _byteVersion = Encoding.ASCII.GetBytes(requestBody);
            request.ContentLength = _byteVersion.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(_byteVersion, 0, _byteVersion.Length);//verify
                stream.Close();
            }
        }

        /// <summary>
        /// Builds the access token request asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        private async Task<HttpWebRequest> BuildAccessTokenRequestAsync(string code)
        {
            string accesstokenRequestBody = string.Format("grant_type=authorization_code&code={0}&redirect_uri={1}",
                code,
                Uri.EscapeDataString(RedirectUri)
                );
            HttpWebRequest accesstokenRequest = await BuildTokenEndpointRequestAsync();

            FormatRequest(accesstokenRequest, accesstokenRequestBody);

            return accesstokenRequest;
        }

        /// <summary>
        /// Builds the refresh token request asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        private async Task<HttpWebRequest> BuildRefreshTokenRequestAsync(string refreshToken)
        {
            // build the  request
            string refreshtokenRequestBody = string.Format("grant_type=refresh_token&refresh_token={0}", refreshToken);

            HttpWebRequest refereshtokenRequest = await BuildTokenEndpointRequestAsync();

            FormatRequest(refereshtokenRequest, refreshtokenRequestBody);

            return refereshtokenRequest;
        }

        /// <summary>
        /// Builds the revoke token request asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        private async Task<HttpWebRequest> BuildRevokeTokenRequestAsync(string refreshToken)
        {
            // build the request
            string tokenRequestBody = string.Format("{\"token\":\"{0}\"}", refreshToken);
            HttpWebRequest request = await BuildRevokeTokenEndpointRequestAsync();
            FormatRequest(request, tokenRequestBody);

            return request;
        }

        /// <summary>
        /// Builds the discovery request.
        /// </summary>
        /// <returns></returns>
        private HttpWebRequest BuildDiscoveryRequest()
        {
            // build the request    
            HttpWebRequest discoveryRequest = (HttpWebRequest)WebRequest.Create(DiscoveryUrl);
            discoveryRequest.Method = Constants.HttpGetReques;
            discoveryRequest.Accept = Constants.HttpJsonContent;
            return discoveryRequest;
        }

        /// <summary>
        /// Gets the token form responce.
        /// </summary>
        /// <param name="responceText">The responce text.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private TokenBaerer GetTokenFormResponce(string responceText)
        {
            TokenBaerer tokenBaerer = new TokenBaerer();
            Dictionary<string, string> refreshTokenDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responceText);

            if (refreshTokenDecoded.ContainsKey(Constants.ErrorKey))
                throw new Exception(string.Concat("error", refreshTokenDecoded[Constants.ErrorKey]));

            if (refreshTokenDecoded.ContainsKey(Constants.AccessTokenKey))
                tokenBaerer.AccessToken = refreshTokenDecoded[Constants.AccessTokenKey];

            if (refreshTokenDecoded.ContainsKey(Constants.RefershTokenKey))
                tokenBaerer.RefreshToken = refreshTokenDecoded[Constants.RefershTokenKey];

            return tokenBaerer;
        }
        #endregion
    }
}
