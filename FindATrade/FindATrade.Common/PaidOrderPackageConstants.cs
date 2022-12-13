using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindATrade.Common
{
    public static class PaidOrderPackageConstants
    {
        public const int NameMin = 5;

        public const int NameMax = 100;

        public const string NameMesage = "{0} must be between {2} and {1} characters";

        public const int PriceMin = 0;

        public const int PriceMax = 10000000;

        public const string PriceMessage = "{0] must be between {1} and 1 million";

        public const int TermsMin = 5;

        public const int TermsMax = 500;

        public const string TermsMesage = "{0} must be between {2} and {1} characters";
    }
}
