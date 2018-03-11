//© 2017 CROWDERIA PVT LTD ALL RIGHTS RESERVED
// Description  Address
// Namespace    QBEntity.QB
// Author       Damitha Shyamantha      Date    12/04/2017

namespace QBEntity.QB
{
    /// <summary>
    /// addres
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the line1.
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country sub division code.
        /// </summary>
        public string CountrySubDivisionCode { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// Gets or sets the long.
        /// </summary>
        public string Long { get; set; }
    }
}
