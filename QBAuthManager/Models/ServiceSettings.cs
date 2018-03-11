//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  ServiceSettings
// Namespace    QBAuthManager.Models
// Author       Damitha Shyamantha      Date    12/04/2017

namespace QBAuthManager.Models
{
    /// <summary>
    /// qb service settings
    /// </summary>
    public class ServiceSettings
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public TokenBaerer Token { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
