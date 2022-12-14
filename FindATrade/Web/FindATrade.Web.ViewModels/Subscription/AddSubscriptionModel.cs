namespace FindATrade.Web.ViewModels.Subscription
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class AddSubscriptionModel : IMapFrom<PaidOrderPackageType>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Terms { get; set; }
    }
}
