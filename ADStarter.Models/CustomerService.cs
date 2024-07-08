#nullable disable

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models
{
    public partial class CustomerService
    {
        [Key]
        public int cs_ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string cs_name { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string cs_phoneNum { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postcode")]
        public string cs_zip { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string cs_street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string cs_city { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string cs_state { get; set; }

        public IdentityUser User { get; set; }
    }
}
