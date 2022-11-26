namespace FindATrade.Data.Models
{
    using FindATrade.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        public int? ServiceId { get; set; }

        public Service Service { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }
    }
}