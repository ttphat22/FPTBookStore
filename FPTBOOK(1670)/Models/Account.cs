
namespace FPTBOOK_1670_.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [Key]
        [StringLength(50)]

        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Th password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation new password do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }


        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        public int State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
