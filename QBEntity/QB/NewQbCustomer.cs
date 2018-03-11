using Newtonsoft.Json;

namespace QBEntity.QB
{
    public class NewQbCustomer
    {
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the given.
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the family.
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the fully qualified.
        /// </summary>
        public string FullyQualifiedName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the print on check.
        /// </summary>
        public string PrintOnCheckName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Customer"/> is active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets the primary phone.
        /// </summary>
        public Phone PrimaryPhone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty("PrimaryEmailAddr")]
        public Email Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Customer"/> is taxable.
        /// </summary>
        public bool Taxable { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        [JsonProperty("BillAddr")]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        public bool Job { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bill with parent].
        /// </summary>
        public bool BillWithParent { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the balance with jobs.
        /// </summary>
        public decimal BalanceWithJobs { get; set; }

        /// <summary>
        /// Gets or sets the preferred delivery method.
        /// </summary>
        public string PreferredDeliveryMethod { get; set; }

        /// <summary>
        /// Gets or sets the domian.
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Customer"/> is sparse.
        /// </summary>
        [JsonProperty("sparse")]
        public bool sparse { get; set; }
    }
}
