//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  QueryResult
// Namespace    QBEntity.System
// Author       Damitha Shyamantha      Date    12/12/2017

#region UsingDirectives
using System; 
#endregion

namespace QBEntity.System
{
    /// <summary>
    /// qb qury result
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// Gets or sets the query response.
        /// </summary>
        public QueryResponse QueryResponse { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
