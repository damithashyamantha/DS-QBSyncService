//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  MetaData
// Namespace    QBEntity.System
// Author       Damitha Shyamantha      Date    12/19/2017

#region UsingDirectives
using System; 
#endregion

namespace QBEntity.System
{
    /// <summary>
    /// meta data
    /// </summary>
    public class MetaData
    {
        #region PublicMethods
        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last updated time.
        /// </summary>
        public DateTime LastUpdatedTime { get; set; } 
        #endregion
    }
}
