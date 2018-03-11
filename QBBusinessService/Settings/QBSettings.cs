// Description  QBSettings
// Namespace    QBBusinessService.Settings
// Author       Damitha Shyamantha      Date    12/18/2017

#region UsingDirectives
using System.Configuration;
#endregion

namespace QBBusinessService.Settings
{
    /// <summary>
    /// configuration for qb integration
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    public class QBSettings : ConfigurationSection
    {
        #region Private members
        private static QBSettings settings = ConfigurationManager.GetSection("QBSettings") as QBSettings;
        #endregion

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public static QBSettings Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Gets or sets the qb client identifier.
        /// </summary>
        [ConfigurationProperty("ClientId", IsRequired = true)]
        public string QBClientId
        {
            get
            {
                return (string)this[Constants.QBClientIdConfigKey];
            }
            set
            {
                this[Constants.QBClientIdConfigKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the qb client secret.
        /// </summary>
        [ConfigurationProperty("ClientSecret", IsRequired = true)]
        public string QBClientSecret
        {
            get
            {
                return (string)this[Constants.QBClientSecretConfigKey];
            }
            set
            {
                this[Constants.QBClientSecretConfigKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the qb discovery URL.
        /// </summary>
        [ConfigurationProperty("DiscoveryUrl", IsRequired = true)]
        public string QBDiscoveryUrl
        {
            get
            {
                return (string)this[Constants.QBDiscoveryUrlConfigKey];
            }
            set
            {
                this[Constants.QBDiscoveryUrlConfigKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the qb redirect URI.
        /// </summary>
        [ConfigurationProperty("RedirectUri", IsRequired = true)]
        public string QBRedirectUri
        {
            get
            {
                return (string)this[Constants.QBQBRedirectUriConfigKey];
            }
            set
            {
                this[Constants.QBQBRedirectUriConfigKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the qb scope.
        /// </summary>
        [ConfigurationProperty("Scope", IsRequired = true)]
        public string QBScope
        {
            get
            {
                return (string)this[Constants.QBScopeConfigKey];
            }
            set
            {
                this[Constants.QBScopeConfigKey] = value;
            }
        }

        /// <summary>
        /// Gets or sets the qb service context base URL.
        /// </summary>
        [ConfigurationProperty("ServiceContextBaseUrl", IsRequired = true)]
        public string QBServiceContextBaseUrl
        {
            get
            {
                return (string)this[Constants.QBServiceContextBaseUrlConfigKey];
            }
            set
            {
                this[Constants.QBServiceContextBaseUrlConfigKey] = value;
            }
        }
    }
}
