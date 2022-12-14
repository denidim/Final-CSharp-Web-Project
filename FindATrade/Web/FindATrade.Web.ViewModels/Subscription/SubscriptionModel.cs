namespace FindATrade.Web.ViewModels.Subscription
{
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SubscriptionModel : IMapFrom<PaidOrder>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Terms { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
