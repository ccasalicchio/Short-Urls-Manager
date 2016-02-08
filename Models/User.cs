using System;
using System.ComponentModel.DataAnnotations;

namespace Shorten_Urls.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        public string Password { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Name { get; set; }
        [Display(Name = "Account Type")]
        public UserTypes UserType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joined On")]
        public DateTime CreatedOn { get; set; }
    }
}
