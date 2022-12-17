namespace FindATrade.Common
{
    public static class PaidOrderConstants
    {
        public const int NameMin = 5;

        public const int NameMax = 100;

        public const string NameMessage = "{0} must be between {2} and {1} characters";

        public const int PriceMin = 0;

        public const int PriceMax = 10000000;

        public const string PriceMessage = "{0] must be between {1} and 1 million";

        public const int TermsMin = 5;

        public const int TermsMax = 500;

        public const string TermsMessage = "{0} must be between {2} and {1} characters";

        public const int TimeSchedule = 3;

        public const decimal Price = 10.00m;

        public const string Name = "Regular";

        public const string Terms = "Your Service will be on the front page and first on serches";
    }
}
