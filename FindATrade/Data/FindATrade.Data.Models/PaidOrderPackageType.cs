namespace FindATrade.Data.Models
{
    using FindATrade.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class PaidOrderPackageType : BaseDeletableModel<int>
    {
        public int PaidOrderId { get; set; }

        public PaidOrder PaidOrders { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Terms { get; set; }
    }
}