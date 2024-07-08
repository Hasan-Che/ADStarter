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

        [DataType(DataType.Date)]
        public DateTime rep_datetime { get; set; }

        public int schedule_ID { get; set; }

        [ForeignKey(nameof(schedule_ID))]
        public virtual Schedule Schedule { get; set; }

        // Added properties
        [StringLength(500)]
        public string rep_remark { get; set; }

        [StringLength(255)]
        public string rep_file { get; set; }

        [Required]
        [StringLength(50)]
        public string rep_status { get; set; } = "Pending"; // Default value set to 'Pending'
    }
}
