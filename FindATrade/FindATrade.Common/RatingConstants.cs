namespace FindATrade.Common
{
    public static class RatingConstants
    {
        public const int DescriptionMin = 10;

        public const string DescriptionMessage = "{0} must be at least {1} characters";

        public const int WorkmanshipMin = 1;

        public const int WorkmanshipMax = 10;

        public const string WorkmanshipName = "Workmanship (1/10)";

        public const string WorkmanshipMessage = "{0} must be between {2} and {1}";

        public const int TidinessMin = 1;

        public const int TidinessMax = 10;

        public const string TidinesName = "Tidiness (1/10)";

        public const string TidinessMessage = "{0} must be between {2} and {1}";

        public const int ReliabilityMin = 1;

        public const int ReliabilityMax = 10;

        public const string ReliabilityName = "Reliability (1/10)";

        public const string ReliabilityMessage = "{0} must be between {2} and {1}";

        public const int CourtesyMin = 1;

        public const int CourtesyMax = 10;

        public const string CourtesyName = "{0} (1/10)";

        public const string CourtesyMessage = "Courtesy must be between {2} and {1}";

        public const int QuoteAccuracyMin = 1;

        public const int QuoteAccuracyMax = 10;

        public const string QuateAccuracyName = "QuoteAccuracy (1/10)";

        public const string QuoteAccuracyMessage = "{0} must be between {2} and {1}";
    }
}
