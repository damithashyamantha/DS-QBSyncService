//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  Company
// Namespace    QBEntity.QB
// Author       Damitha Shyamantha      Date    12/04/2017

#region UsingDirectives
using Newtonsoft.Json;
using QBEntity.System;
using System.Collections.Generic; 
#endregion

namespace QBEntity.QB
{
    public class Company : BaseEntity
    {
        #region PublicMethods
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the legal.
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// Gets or sets the company address.
        /// </summary>
        [JsonProperty("CompanyAddr")]
        public Address CompanyAddress { get; set; }

        /// <summary>
        /// Gets or sets the communication address.
        /// </summary>
        [JsonProperty("CustomerCommunicationAddr")]
        public Address CommunicationAddress { get; set; }

        /// <summary>
        /// Gets or sets the legal address.
        /// </summary>
        [JsonProperty("LegalAddr")]
        public Address LegalAddress { get; set; }

        /// <summary>
        /// Gets or sets the company start date.
        /// </summary>
        public string CompanyStartDate { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year start month.
        /// </summary>
        public string FiscalYearStartMonth { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public Email Email { get; set; }

        /// <summary>
        /// Gets or sets the web address.
        /// </summary>
        [JsonProperty("WebAddr")]
        public string WebAddress { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Company"/> is sparse.
        /// </summary>
        [JsonProperty("sparse")]
        public bool Sparse { get; set; }

        /// <summary>
        /// Gets or sets the name value.
        /// </summary>
        public List<NameValue> NameValue { get; set; } 
        #endregion
    }
}
