#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class Report
    {
        [Key]
        public int rep_ID { get; set; }

        [StringLength(100)]
        public string rep_title { get; set; }

        public DateTime rep_datetime { get; set; }

        public int c_myKid { get; set; }

        public int t_ID { get; set; }

        [ForeignKey(nameof(c_myKid))]
        public virtual Child Child { get; set; }

        [ForeignKey(nameof(t_ID))]
        public virtual Therapist Therapist { get; set; }
    }
}
