#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Invoice
    {
        [Key]
        public int invoice_ID { get; set; }

        public string c_myKid { get; set; }

        public int schedule_ID { get; set; }

        public DateTime due_date { get; set; }

        public double invoice_amount { get; set; }

        [ForeignKey(nameof(c_myKid))]
        public virtual Child Child { get; set; }

        [ForeignKey(nameof(schedule_ID))]
        public virtual Schedule Schedule { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
