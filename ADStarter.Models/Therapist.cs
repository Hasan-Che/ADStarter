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

        [StringLength(100)]
        public string t_name { get; set; }

        [StringLength(15)]
        public string t_phoneNum { get; set; }

        [StringLength(255)]
        public string t_address { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
