namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Workmanship { get; set; }

        [Required]
        public int Tidiness { get; set; }

        [Required]
        public int Reliability { get; set; }

        [Required]
        public int Courtesy { get; set; }

        [Required]
        public int QuoteAccuracy { get; set; }
    }
}
