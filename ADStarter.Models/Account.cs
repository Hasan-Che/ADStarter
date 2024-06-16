#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Account
    {
        [Key]
        public int acc_ID { get; set; }

        [StringLength(100)]
        public string acc_email { get; set; }

        [StringLength(100)]
        public string acc_pass { get; set; }

        [StringLength(50)]
        public string acc_status { get; set; }

        public int? astype_ID { get; set; }

        [ForeignKey(nameof(astype_ID))]
        public virtual AccType AccType { get; set; }

        public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();
        public virtual ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();
        public virtual Parent Parent { get; set; }
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Therapist> Therapists { get; set; } = new List<Therapist>();
    }
}
