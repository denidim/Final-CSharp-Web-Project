namespace FindATrade.Data.Models
{
    using FindATrade.Data.Common.Models;

    public class Package : BaseDeletableModel<int>
    {
        public decimal? Price { get; set; }

        public string Description { get; set; }

        public int? ServiceId { get; set; }

        public Service Service { get; set; }
    }
}
