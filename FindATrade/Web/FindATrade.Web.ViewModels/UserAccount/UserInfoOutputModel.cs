namespace FindATrade.Web.ViewModels.UserAccount
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class UserInfoOutputModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}