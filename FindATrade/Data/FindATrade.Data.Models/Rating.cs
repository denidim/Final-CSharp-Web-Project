namespace FindATrade.Data.Models
{
    using FindATrade.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Rating : BaseDeletableModel<int>
    {
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public int CompanyId { get; set; }

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
