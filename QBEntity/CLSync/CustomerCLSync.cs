using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBEntity.CLSync
{
    public class CustomerCLSync
    {
        /// <summary>
        /// Gets or sets the qb identifier.
        /// </summary>
        public int QBId { get; set; }

        /// <summary>
        /// Gets or sets the logistic identifier.
        /// </summary>
        public string LogisticId { get; set; }

        /// <summary>
        /// Gets or sets the synchronize token.
        /// </summary>
        public int SyncToken { get; set; }
    }
}
