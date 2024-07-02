#nullable disable
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Therapist
    {
        [Key]
        public int t_ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string t_name { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string t_phoneNum { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string t_street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string t_city { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postcode")]
        public string t_zip { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string t_state { get; set; }

        [StringLength(450)]
        public IdentityUser User { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();


      
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();


    }
}
