namespace FindATrade.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Company : BaseDeletableModel<int>
    {
        public Company()
        {
            this.Services = new HashSet<Service>();
            this.Skills = new HashSet<Skill>();
            this.Ratings = new HashSet<Rating>();
            this.Likes = new HashSet<Like>();
        }

        [Required]
        [StringLength(CompanyConstants.NameMax, MinimumLength = CompanyConstants.NameMin, ErrorMessage = CompanyConstants.NameMessage)]
        public string Name { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [StringLength(CompanyConstants.WebsiteMax, MinimumLength = CompanyConstants.WebsiteMin, ErrorMessage = CompanyConstants.WebsiteMessage)]
        public string Website { get; set; }

        [Required]
        [StringLength(CompanyConstants.EmailMax, MinimumLength = CompanyConstants.EmailMax, ErrorMessage = CompanyConstants.EmailMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        [StringLength(CompanyConstants.PhoneNumberMax, MinimumLength = CompanyConstants.PhoneNumberMin, ErrorMessage = CompanyConstants.PhoneNumberMessage)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(CompanyConstants.DescriptionMin, ErrorMessage = CompanyConstants.DescriptionMessage)]
        public string Description { get; set; }

        public Image Image { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
