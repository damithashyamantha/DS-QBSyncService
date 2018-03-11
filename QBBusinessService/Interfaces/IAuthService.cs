// Description  IAuthService
// Namespace    QBBusinessService.Interfaces
// Author       Damitha Shyamantha      Date    12/17/2017

#region UsingDirectives
using QBAuthManager.Models;
using System.Threading.Tasks;
#endregion

namespace QBBusinessService.Interfaces
{
    /// <summary>
    /// interface for QB auth services
    /// </summary>
    /// <seealso cref="QBBusinessService.Interfaces.IServiceUnit" />
    public interface IAuthService : IServiceUnit
    {
        /// <summary>
        /// Gets the authentication request asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthRequestAsync();

        /// <summary>
        /// Gets the new token asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="code">The code.</param>
        /// <param name="realmId">The realm identifier.</param>
        /// <returns></returns>
        Task<TokenBaerer> GetNewTokenAsync(string state, string code, string realmId);

        /// <summary>
        /// Refreshes the token asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<TokenBaerer> RefreshTokenAsync();

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <returns></returns>
        TokenBaerer RefreshToken();

        /// <summary>
        /// Gets the authentication token asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<TokenBaerer> GetAuthTokenAsync();

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns></returns>
        TokenBaerer GetAuthToken();
    }
}
