#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Admin
    {
        [Key]
        public int a_ID { get; set; }

        [StringLength(100)]
        public string a_name { get; set; }

        public int acc_ID { get; set; }

        [ForeignKey(nameof(acc_ID))]
        public virtual Account Account { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
