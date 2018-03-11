//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  DiscoveryData
// Namespace    QBEntity.QB
// Author       Damitha Shyamantha      Date    12/12/2017

#region UsingDirectives
using Newtonsoft.Json;
using System.Collections.Generic; 
#endregion

namespace QBEntity.QB
{
    /// <summary>
    /// discovery data
    /// </summary>
    public class DiscoveryData
    {
        #region PublicProperties
        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the authorization endpoint.
        /// </summary>
        [JsonProperty("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint.
        /// </summary>
        [JsonProperty("token_endpoint")]
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the userinfo endpoint.
        /// </summary>
        [JsonProperty("userinfo_endpoint")]
        public string UserinfoEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the revocation endpoint.
        /// </summary>
        [JsonProperty("revocation_endpoint")]
        public string RevocationEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the JWKS URI.
        /// </summary>
        [JsonProperty("jwks_uri")]
        public string JWKSUri { get; set; }

        /// <summary>
        /// Gets or sets the response type supported.
        /// </summary>
        [JsonProperty("response_types_supported")]
        public List<string> ResponseTypeSupported { get; set; }

        /// <summary>
        /// Gets or sets the subject types supported.
        /// </summary>
        [JsonProperty("subject_types_supported")]
        public List<string> SubjectTypesSupported { get; set; }

        /// <summary>
        /// Gets or sets the identifier token signing alg values supported.
        /// </summary>
        [JsonProperty("id_token_signing_alg_values_supported")]
        public List<string> IdTokenSigningAlgValuesSupported { get; set; }

        /// <summary>
        /// Gets or sets the scopes supported.
        /// </summary>
        [JsonProperty("scopes_supported")]
        public List<string> ScopesSupported { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint authentication methods supported.
        /// </summary>
        [JsonProperty("token_endpoint_auth_methods_supported")]
        public List<string> TokenEndpointAuthMethodsSupported { get; set; }

        /// <summary>
        /// Gets or sets the claims supported.
        /// </summary>
        [JsonProperty("claims_supported")]
        public List<string> ClaimsSupported { get; set; } 
        #endregion
    }
}
