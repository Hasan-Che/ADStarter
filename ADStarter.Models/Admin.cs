#nullable disable
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Admin
    {
        [Key]
        public int a_ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string a_name { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string a_street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string a_city { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postcode")]
        public string a_zip { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string a_state { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string a_phoneNum { get; set; }

        public IdentityUser User { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
