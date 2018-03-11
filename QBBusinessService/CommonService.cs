// Description  CommonService
// Namespace    QBBusinessService
// Author       Damitha Shyamantha      Date    12/15/2017

#region UsingDirectives
using QBAuthManager.Models;
using QBBusinessService.Settings;
using QBEntity.System;
using System.Text;
#endregion

namespace QBBusinessService
{
    /// <summary>
    /// service class which implements common services
    /// </summary>
    public static class CommonService
    {
        #region PrivateMembers
        private static Encoding encoder; 
        #endregion

        #region PublicProperties
        /// <summary>
        /// Gets the encoder.
        /// </summary>
        public static Encoding Encoder
        {
            get
            {
                if (encoder == null)
                    encoder = Encoding.UTF8;
                return encoder;
            }
        } 
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the qb authentication settings.
        /// </summary>
        /// <returns></returns>
        public static OAuthSettings GetQBAuthSettings()
        {
            var authSettings = new OAuthSettings();

            authSettings.ClientId = QBSettings.Settings.QBClientId;
            authSettings.ClientSecret = QBSettings.Settings.QBClientSecret;
            authSettings.DiscoveryUrl = QBSettings.Settings.QBDiscoveryUrl;
            authSettings.RedirectUri = QBSettings.Settings.QBRedirectUri;
            authSettings.Scope = QBSettings.Settings.QBScope;
            authSettings.ServiceContextBaseUrl = QBSettings.Settings.QBServiceContextBaseUrl;

            return authSettings;
        }

        /// <summary>
        /// Gets the qb service settings.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static ServiceSettings GetQBServiceSettings(TokenBaerer token)
        {
            var serviceSettings = new ServiceSettings();

            serviceSettings.BaseUrl = QBSettings.Settings.QBServiceContextBaseUrl;
            serviceSettings.Token = token;

            return serviceSettings;
        }
        #endregion
    }
}
