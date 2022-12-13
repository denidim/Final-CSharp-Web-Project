namespace FindATrade.Common
{
    public static class AddressConstants
    {
        public const int StreetMin = 2;

        public const int StreetMax = 50;

        public const string StreetMessage = "{0} must be between {2} and {1} cahracters";

        public const int HouseMin = 0;

        public const int HouseMax = 1000;

        public const string HouseName = "House/Flat Number";

        public const string HouseMessage = "{0} must be between {1} and {2}";

        public const int HouseAdditionMin = 3;

        public const int HouseAdditionMax = 50;

        public const string HouseAdditinName = "Addition (optional)";

        public const string HouseAdditionMessage = "{0} must be between {2} and {1} cahracters";

        public const int CityMin = 3;

        public const int CityMax = 50;

        public const string CityMessage = "{0} must be between {2} and {1} cahracters";

        public const int CountryMin = 3;

        public const int CountryMax = 50;

        public const string CountryMessage = "{0} must be between {2} and {1} cahracters";

        public const string PostalCodeName = "Postal Code (optional)";
    }
}
