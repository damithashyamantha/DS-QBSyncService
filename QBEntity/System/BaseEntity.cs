//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  BaseModel
// Namespace    QBEntity.System
// Author       Damitha Shyamantha      Date    12/19/2017

#region USingDirective
using Newtonsoft.Json; 
#endregion

namespace QBEntity.System
{
    /// <summary>
    /// base qb model
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the synchronize token.
        /// </summary>
        public int SyncToken { get; set; }

        /// <summary>
        /// Gets or sets the m eta data.
        /// </summary>
        public MetaData MetaData { get; set; }

    }
}