using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]        
        public string UserName { get; set; }

      /*  [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        */

        public string UserEmail { get; set; }               
        public string Name { get; set; }


        public DateTime RegisterDate { get; set; }

        public Nullable<int> CountryId { get; set; }
        public virtual Country Country { get; set; }

    }
}