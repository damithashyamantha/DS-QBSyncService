//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  IdTokenHeader
// Namespace    QBEntity.QB
// Author       Damitha Shyamantha      Date    12/07/2017

#region UsingDirectives
using Newtonsoft.Json; 
#endregion

namespace QBEntity.QB
{
    /// <summary>
    /// Id token header model
    /// </summary>
    public class IdTokenHeader
    {
        #region PublicMethods
        /// <summary>
        /// Gets or sets the kid.
        /// </summary>
        [JsonProperty("kid")]
        public string Kid { get; set; }

        /// <summary>
        /// Gets or sets the alg.
        /// </summary>
        [JsonProperty("alg")]
        public string Alg { get; set; } 
        #endregion
    }
}
