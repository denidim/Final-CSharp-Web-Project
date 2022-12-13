namespace FindATrade.Web.ViewModels.AccountManagement
{
    using FindATrade.Common;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.FirstNameMax, MinimumLength = UserConstants.FirstNameMin, ErrorMessage = UserConstants.FirstNameMessage)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.LastNameMax, MinimumLength = UserConstants.LastNameMin, ErrorMessage = UserConstants.LastNameMessage)]
        public string LastName { get; set; } = null!;
    }
}
