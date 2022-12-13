namespace FindATrade.Web.ViewModels.AccountManagement
{
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;

    public class VettModel
    {
        public bool IsPassed { get; set; }

        [StringLength(VettingConstants.DescriptionMax, MinimumLength = VettingConstants.DescriptionMin, ErrorMessage = VettingConstants.DescriptionMessage)]
        public string Description { get; set; }
    }
}
