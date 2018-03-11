// Description  Constants
// Namespace    QBAuthManager.Helpers
// Author       Damitha Shyamantha      Date    12/07/2017

namespace QBAuthManager.Helpers
{
    /// <summary>
    /// constants
    /// </summary>
    internal static class Constants
    {
        #region InternalConstants
        internal static readonly string HttpGetReques = "GET";
        internal static readonly string HttpPostReques = "POST";

        internal static readonly string HttpJsonContent = "application/json";
        internal static readonly string UrlEncodedContent = "application/x-www-form-urlencoded";
        internal static readonly string HTTPJsonUtf8 = "application/json;charset=UTF-8";
        internal static readonly string AllowAllHeader = "*/*";

        internal static readonly string IdTokenKey = "id_token";
        internal static readonly string AccessTokenKey = "access_token";
        internal static readonly string RefershTokenKey = "refresh_token";
        internal static readonly string ErrorKey = "error";

        internal static readonly string CustomerSuffix = "customer";
        #endregion
    }
}
