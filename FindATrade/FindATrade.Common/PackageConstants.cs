namespace FindATrade.Common
{
    public static class PackageConstants
    {
        public const int PriceMin = 0;

        public const int PriceMax = 100000000;

        public const string PriceName = "Price of service e.g. 100.00";

        public const string PriceMessage = "{0} must be between {1} and 10 million";

        public const int DescriptionMin = 5;

        public const int DescriptionMax = 500;

        public const string DescriptionName = "Description of ofered service for that price";

        public const string DescriptionMessage = "{0} must be between {2} and {1} characters";
    }
}
