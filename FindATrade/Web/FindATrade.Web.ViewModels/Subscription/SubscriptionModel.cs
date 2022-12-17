namespace FindATrade.Web.ViewModels.Subscription
{
    using System;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class SubscriptionModel : IMapFrom<PaidOrder>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Terms { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
