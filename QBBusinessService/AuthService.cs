// Description  AuthService
// Namespace    QBBusinessService
// Author       Damitha Shyamantha      Date    12/17/2017

#region UsingDirectives
using QBAuthManager;
using QBAuthManager.Models;
using QBBusinessService.Interfaces;
using QBBusinessService.System;
using QBEntity.System;
using RavenHandler;
using System;
using System.Threading.Tasks;
#endregion

namespace QBBusinessService
{
    /// <summary>
    /// instance of auth service as a singelton
    /// </summary>
    /// <seealso cref="QBBusinessService.Interfaces.IAuthService" />
    public class AuthService : BaseService, IAuthService
    {
        #region PrivateMembers
        private AuthManager authManager;
        private static AuthService instance;
        private static TokenBaerer token;
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static AuthService Instance
        {
            get
            {
                if (instance == null)
                    instance = new AuthService();
                return instance;
            }
        } 
        #endregion

        #region Construtor
        /// <summary>
        /// Prevents a default instance of the <see cref="AuthService"/> class from being created.
        /// </summary>
        private AuthService()
        {
            var authSettings = CommonService.GetQBAuthSettings();
            authManager = new AuthManager(authSettings);
            token = GetTokenFormDb();
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAuthRequestAsync()
        {
            await authManager.GetDiscoveryDataAsync();
            return authManager.GetAuthRequest();
        }

        /// <summary>
        /// Gets the new token.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="code">The code.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Error in getting token
        /// or
        /// CSRF validation fail
        /// </exception>
        public async Task<TokenBaerer> GetNewTokenAsync(string state, string code, string realmId)
        {
            string csrf = authManager.GetCSRFToken();
            bool isValid = string.Equals(csrf, state);

            if (isValid)
            {
                var token = await authManager.GetTokensAsync(code, realmId);

                if (token == null)
                    throw new Exception("Error in getting token");

                // store token in raven db
                using (RavenManager rm = RavenManager.Instance)
                {
                    // get existing token form raven
                    var oldToken = await rm.GetLatestTokenAsync();

                    // if exist update that token
                    if (oldToken != null)
                    {
                        token.Id = oldToken.Id;
                        await rm.UpdateTokenAsync(token);
                    }
                    // if not store token
                    else
                        await rm.StoreTokenAsync(token);
                }
                return token;
            }
            else
                throw new Exception("CSRF validation fail");
        }

        /// <summary>
        /// Refreshes the token asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">No existing auth token in DB</exception>
        public async Task<TokenBaerer> RefreshTokenAsync()
        {
            using (RavenManager rm = RavenManager.Instance)
            {
                TokenBaerer oldToken = await rm.GetLatestTokenAsync();
                if (oldToken == null)
                    throw new Exception("No existing auth token in DB");

                token = await authManager.RefreshTokenAsync(oldToken.RefreshToken);

                token.Id = oldToken.Id;
                token.RealmId = oldToken.RealmId;
                token.ExpiaryDate = oldToken.ExpiaryDate;

                await rm.UpdateTokenAsync(token);
                return token;
            }
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">No existing auth token in DB</exception>
        public TokenBaerer RefreshToken()
        {
            using (RavenManager rm = RavenManager.Instance)
            {
                TokenBaerer oldToken = rm.GetLatestToken();
                if (oldToken == null)
                    throw new Exception("No existing auth token in DB");

                token = authManager.RefreshToken(oldToken.RefreshToken);

                token.Id = oldToken.Id;
                token.RealmId = oldToken.RealmId;
                token.ExpiaryDate = oldToken.ExpiaryDate;

                rm.UpdateToken(token);
                return token;
            }
        }

        /// <summary>
        /// Gets the authentication token asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">No existing auth token in DB</exception>
        public async Task<TokenBaerer> GetAuthTokenAsync()
        {
            using (RavenManager rm = RavenManager.Instance)
            {
                var token = await rm.GetLatestTokenAsync();

                return token;
            }
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        public TokenBaerer GetAuthToken()
        {
            if (token == null)
                token = GetTokenFormDb();
            return token;
        }
        #endregion

        #region PrivateMethods        
        /// <summary>
        /// Gets the token form database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">No existing auth token in DB</exception>
        private TokenBaerer GetTokenFormDb()
        {
            using (RavenManager rm = RavenManager.Instance)
            {
                token = rm.GetLatestToken();

                //if (token == null)
                //    throw new Exception("No existing auth token in DB");
            }
            return token;
        } 
        #endregion
    }
}
