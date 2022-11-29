namespace FindATrade.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Data.Models;
    using FindATrade.Services.Mapping;

    public class ReviewModel : IMapFrom<Rating>
    {
        [Required]
        [MinLength(10, ErrorMessage = "Minumum 10 cahrace requred")]
        public string Description { get; set; }

        [Range(1, 10)]
        [Required]
        public int Workmanship { get; set; }

        [Range(1, 10)]
        [Required]
        public int Tidiness { get; set; }

        [Range(1, 10)]
        [Required]
        public int Reliability { get; set; }

        [Range(1, 10)]
        [Required]
        public int Courtesy { get; set; }

        [Range(1, 10)]
        [Required]
        public int QuoteAccuracy { get; set; }

    }
}
