#nullable disable
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

        public int acc_ID { get; set; }

        [ForeignKey(nameof(acc_ID))]
        public virtual Account Account { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
