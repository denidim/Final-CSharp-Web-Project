namespace FindATrade.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Services = new HashSet<Service>();
        }

        [Required]
        [StringLength(CategoryConstants.CategoryMax, MinimumLength = CategoryConstants.CategoryMin, ErrorMessage = CategoryConstants.CategoryMessage)]
        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
