namespace FindATrade.Common
{
    public class CompanyConstants
    {
        public const int NameMin = 3;

        public const int NameMax = 100;

        public const string NameMessage = "{0} must be between {2} and {1} characters";

        public const int WebsiteMin = 6;

        public const int WebsiteMax = 50;

        public const string WebsiteMessage = "{0} must be between {2} and {1} characters";

        public const int EmailMin = 6;

        public const int EmailMax = 100;

        public const string EmailMessage = "{0} must be between {2} and {1} characters";

        public const int PhoneNumberMin = 7;

        public const int PhoneNumberMax = 15;

        public const string PhoneNumberName = "Company Phone";

        public const string PhoneNumberMessage = "{0} must be between {2} and {1} characters";

        public const int DescriptionMin = 50;

        public const string DescriptionName = "Company Description";

        public const string DescriptionMessage = "{0} must be at least {1} characters";
    }
}
