namespace FindATrade.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class ReviewModel : IMapFrom<Rating>
    {
        [Required]
        [MinLength(10, ErrorMessage = "{0} must be at least {1} characters")]
        public string Description { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required]
        public int Workmanship { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required]
        public int Tidiness { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required]
        public int Reliability { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required]
        public int Courtesy { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [Required]
        public int QuoteAccuracy { get; set; }

    }
}
