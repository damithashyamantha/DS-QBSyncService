//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  OAuthSettings
// Namespace    QBAuthManager.Models
// Author       Damitha Shyamantha      Date    12/05/2017

namespace QBEntity.System
{
    /// <summary>
    /// auth settings
    /// </summary>
    public class OAuthSettings
    {
        #region PublicProperties
        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the discovery URL.
        /// </summary>
        public string DiscoveryUrl { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the service context base URL.
        /// </summary>
        public string ServiceContextBaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// ex OidcScopes.Accounting.GetStringValue();
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        public string LogPath { get; set; }
        #endregion
    }
}
