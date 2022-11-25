namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePackageInputModel
    {
        [Required]
        [Display(Name = "Price of service e.g. 100.00 lv")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Description of ofered service for that price")]
        public string Descrtiption { get; set; }
    }
}
