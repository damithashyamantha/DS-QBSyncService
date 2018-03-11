//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  TokenBaerer
// Namespace    QBAuthManager.Models
// Author       Damitha Shyamantha      Date    12/04/2017

using System;

namespace QBAuthManager.Models
{
    /// <summary>
    /// token baerer
    /// </summary>
    public class TokenBaerer
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets the identity token.
        /// </summary>
        public string IdentityToken { get; internal set; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        public string RefreshToken { get; internal set; }

        /// <summary>
        /// Gets or sets the expiary date.
        /// </summary>
        public DateTime ExpiaryDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the realm identifier.
        /// </summary>
        public string RealmId { get; set; }
    }
}
