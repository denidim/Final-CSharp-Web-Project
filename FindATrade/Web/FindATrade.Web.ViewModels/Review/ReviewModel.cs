namespace FindATrade.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class ReviewModel : IMapFrom<Rating>
    {
        [Required]
        [MinLength(RatingConstants.DescriptionMin, ErrorMessage = RatingConstants.DescriptionMessage)]
        public string Description { get; set; }

        [Range(RatingConstants.WorkmanshipMin, RatingConstants.CourtesyMax, ErrorMessage = RatingConstants.WorkmanshipMessage)]
        [Required]
        public int Workmanship { get; set; }

        [Range(RatingConstants.TidinessMin, RatingConstants.TidinessMax, ErrorMessage = RatingConstants.TidinessMessage)]
        [Required]
        public int Tidiness { get; set; }

        [Range(RatingConstants.ReliabilityMin, RatingConstants.ReliabilityMax, ErrorMessage = RatingConstants.ReliabilityMessage)]
        [Required]
        public int Reliability { get; set; }

        [Range(RatingConstants.CourtesyMin, RatingConstants.CourtesyMax, ErrorMessage = RatingConstants.CourtesyMessage)]
        [Required]
        public int Courtesy { get; set; }

        [Range(RatingConstants.QuoteAccuracyMin, RatingConstants.QuoteAccuracyMax, ErrorMessage = RatingConstants.QuoteAccuracyMessage)]
        [Required]
        public int QuoteAccuracy { get; set; }

    }
}
