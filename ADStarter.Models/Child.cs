#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Child
    {
        [Key]
        public int c_myKid { get; set; }

        public int? prog_ID { get; set; }
        public int? t_ID { get; set; }

        public int parent_ID { get; set; }

        [StringLength(100)]
        public string c_name { get; set; }

        public int c_age { get; set; }

        [StringLength(10)]
        public string c_gender { get; set; }

        public DateTime c_dob { get; set; }

        [StringLength(50)]
        public string c_nationality { get; set; }

        [StringLength(50)]
        public string c_religion { get; set; }

        [StringLength(50)]
        public string c_race { get; set; }

        [StringLength(50)]
        public string c_status { get; set; }

        [StringLength(255)] // Adjust length based on your needs
        public string c_photo { get; set; } // Change to string type

        [ForeignKey(nameof(prog_ID))]
        public virtual Program Program { get; set; }

        [ForeignKey("parent_ID")]
        public virtual Parent Parent { get; set; }

        [ForeignKey("t_ID")]
        public virtual Therapist Therapist { get; set; }

        public virtual TreatmentHistory? TreatmentHistory { get; set; }
        public virtual ICollection<Schedule>? Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<Report>? Reports { get; set; } = new List<Report>();
        public virtual ICollection<Invoice>? Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
    }
}
