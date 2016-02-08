using System;
using System.ComponentModel.DataAnnotations;

namespace Shorten_Urls.Models
{
    public class Url
    {
        public int Id { get; set; }
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Minimum 8 characters required")]
        [Required(ErrorMessage = "Required")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Invalid")]
        [Display(Name ="Shareable Url")]
        public string Src { get; set; }
        [Url]
        [Display(Name = "Redirects To")]
        public string Redirect { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Created By")]
        public string Username { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expires")]
        public DateTime? Expires { get; set; }
    }
}
