namespace FindATrade.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Services = new HashSet<Service>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
