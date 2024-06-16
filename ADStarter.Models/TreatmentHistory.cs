#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class TreatmentHistory
    {
        [Key]
        public int c_myKid { get; set; }

        [StringLength(100)]
        public string th_pediatrician { get; set; }

        [StringLength(255)]
        public string th_recommendation { get; set; }

        public DateTime th_deadline { get; set; }

        [StringLength(255)]
        public string th_diagnosis { get; set; }

        [StringLength(255)]
        public string th_prevTherapy { get; set; }

        [ForeignKey(nameof(c_myKid))]
        public virtual Child Child { get; set; }
    }
}
