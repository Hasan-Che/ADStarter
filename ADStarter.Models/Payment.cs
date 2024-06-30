#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ADStarter.Models
{
    public partial class Payment
    {
        [Key]
        public int pay_ID { get; set; }

        public int parent_ID { get; set; }

        public double pay_amount { get; set; }

        public double pay_balance { get; set; }

        public DateTime pay_date { get; set; }

        [StringLength(255)]
        public string pay_desc { get; set; } // Status of payment: pending or paid

        public string stripe_charge_id { get; set; } // Stripe charge ID

        public string c_myKid { get; set; }

        public string Id { get; set; } // User ID from AspNetUsers

        public int invoice_ID { get; set; }

        [ForeignKey(nameof(c_myKid))]
        public virtual Child Child { get; set; }

        [ForeignKey("parent_ID")]
        public virtual Parent Parent { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual IdentityUser User { get; set; }

        [ForeignKey(nameof(invoice_ID))]
        public virtual Invoice Invoice { get; set; }
    }
}
