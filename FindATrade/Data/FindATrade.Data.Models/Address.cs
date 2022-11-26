namespace FindATrade.Data.Models
{
    using System.Collections.Generic;

    using FindATrade.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public string HouseNumberAddition { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public Company Company { get; set; }
    }
}