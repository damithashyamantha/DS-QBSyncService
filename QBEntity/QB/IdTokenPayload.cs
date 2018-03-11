//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  IdTokenPayload
// Namespace    QBEntity.QB
// Author       Damitha Shyamantha      Date    12/07/2017

#region UsingDirectives
using Newtonsoft.Json;
using System.Collections.Generic; 
#endregion

namespace QBEntity.QB
{
    /// <summary>
    /// payload model
    /// </summary>
    public class IdTokenPayload
    {
        /// <summary>
        /// Gets or sets the sub.
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the aud.
        /// </summary>
        [JsonProperty("aud")]
        public List<string> Aud { get; set; }

        /// <summary>
        /// Gets or sets the realm identifier.
        /// </summary>
        [JsonProperty("realmId")]
        public string RealmId { get; set; }

        /// <summary>
        /// Gets or sets the authentication time.
        /// </summary>
        [JsonProperty("auth_time")]
        public string Auth_time { get; set; }

        /// <summary>
        /// Gets or sets the iss.
        /// </summary>
        [JsonProperty("iss")]
        public string Iss { get; set; }

        /// <summary>
        /// Gets or sets the exp.
        /// </summary>
        [JsonProperty("exp")]
        public string Exp { get; set; }

        /// <summary>
        /// Gets or sets the iat.
        /// </summary>
        [JsonProperty("iat")]
        public string Iat { get; set; }
    }
}
