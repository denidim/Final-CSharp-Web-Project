namespace FindATrade.Web.ViewModels.UserAccount
{
    using System.Collections.Generic;

    public class UserAccountOutputModel
    {
        public UserInfoOutputModel UserInfo { get; set; }

        public UserCompany UserCompany { get; set; }

        public IEnumerable<UserCompanyServices> UserCompanyServices { get; set; }
    }
}
