// ReSharper disable VirtualMemberCallInConstructor
namespace FindATrade.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Common;
    using FindATrade.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Likes = new HashSet<Like>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        [StringLength(UserConstants.FirstNameMax, MinimumLength = UserConstants.FirstNameMin, ErrorMessage = UserConstants.FirstNameMessage)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(UserConstants.LastNameMax, MinimumLength = UserConstants.LastNameMin, ErrorMessage = UserConstants.LastNameMessage)]
        public string LastName { get; set; }

        public Company Company { get; set; }

        public Employee Employee { get; set; }

        public Rating Ratings { get; set; }

        public ICollection<Like> Likes { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
