namespace FindATrade.Data.Models
{
    using FindATrade.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
