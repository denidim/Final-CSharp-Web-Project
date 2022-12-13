namespace FindATrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }

        [Required]
        [MinLength(RatingConstants.DescriptionMin, ErrorMessage = RatingConstants.DescriptionMessage)]
        public string Description { get; set; }

        [Required]
        [Range(RatingConstants.WorkmanshipMin, RatingConstants.CourtesyMax, ErrorMessage = RatingConstants.WorkmanshipMessage)]
        public int Workmanship { get; set; }

        [Required]
        [Range(RatingConstants.TidinessMin, RatingConstants.TidinessMax, ErrorMessage = RatingConstants.TidinessMessage)]
        public int Tidiness { get; set; }

        [Required]
        [Range(RatingConstants.ReliabilityMin, RatingConstants.ReliabilityMax, ErrorMessage = RatingConstants.ReliabilityMessage)]
        public int Reliability { get; set; }

        [Required]
        [Range(RatingConstants.CourtesyMin, RatingConstants.CourtesyMax, ErrorMessage = RatingConstants.CourtesyMessage)]
        public int Courtesy { get; set; }

        [Required]
        [Range(RatingConstants.QuoteAccuracyMin, RatingConstants.QuoteAccuracyMax, ErrorMessage = RatingConstants.QuoteAccuracyMessage)]
        public int QuoteAccuracy { get; set; }
    }
}
