//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  QBResponce
// Namespace    QBEntity.System
// Author       Damitha Shyamantha      Date    12/12/2017

#region UsingDirectives
using System.Net; 
#endregion

namespace QBEntity.System
{
    /// <summary>
    /// qu responce
    /// </summary>
    public class QBResponce
    {
        #region PublicPorperties
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the reponce.
        /// </summary>
        public string Reponce { get; set; } 
        #endregion
    }
}
