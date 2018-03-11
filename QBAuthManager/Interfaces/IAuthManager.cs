// Description  IAuthManager
// Namespace    QBAuthManager.Interfaces
// Author       Damitha Shyamantha      Date    12/07/2017

#region UsingDirectives
using QBAuthManager.Models;
using QBEntity.QB;
using QBEntity.System;
using System.Net;
using System.Threading.Tasks;
#endregion

namespace QBAuthManager.Interfaces
{
    /// <summary>
    /// interface for auth manager
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        /// Gets the discovery data asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<DiscoveryData> GetDiscoveryDataAsync();

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        string GetAuthRequest();

        /// <summary>
        /// Gets the tokens asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <returns></returns>
        Task<TokenBaerer> GetTokensAsync(string code, string realmId);

        /// <summary>
        /// Refreshes the token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        Task<TokenBaerer> RefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Revokes the token asynchronous.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        Task<HttpStatusCode> RevokeTokenAsync(string accessToken, string refreshToken);

        /// <summary>
        /// Determines whether [is valid identifier token] [the specified identifier token].
        /// </summary>
        /// <param name="idToken">The identifier token.</param>
        /// <returns>
        ///   <c>true</c> if [is valid identifier token] [the specified identifier token]; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidIdToken(string idToken);
    }
}
