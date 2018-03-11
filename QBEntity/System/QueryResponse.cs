//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  QueryResponse
// Namespace    QBEntity.System
// Author       Damitha Shyamantha      Date    12/12/2017

#region UsingDirectives
using QBEntity.QB; 
#endregion

namespace QBEntity.System
{
    /// <summary>
    /// qb query responce
    /// </summary>
    public class QueryResponse
    {
        #region PublicProperties
        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public Customer[] Customer { get; set; }

        /// <summary>
        /// Gets or sets the start position.
        /// </summary>
        public int StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the maximum results.
        /// </summary>
        public int MaxResults { get; set; } 
        #endregion
    }
}
