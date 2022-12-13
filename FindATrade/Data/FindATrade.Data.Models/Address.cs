namespace FindATrade.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Data.Common.Models;
    using static FindATrade.Common.GlobalConstants;

    public class Address : BaseDeletableModel<int>
    {
        [StringLength(AddressConstants.StreetMax, MinimumLength = AddressConstants.StreetMin, ErrorMessage = AddressConstants.StreetMessage)]
        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public string HouseNumberAddition { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public Company Company { get; set; }
    }
}