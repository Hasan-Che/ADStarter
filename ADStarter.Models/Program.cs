#nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models
{
    public partial class Program
    {
        [Key]
        public int prog_ID { get; set; }

        [StringLength(100)]
        public string prog_name { get; set; }

        [StringLength(255)]
        public string prog_desc { get; set; }

        public string prog_summary { get; set; }

        // Removed property
        // public double prog_price { get; set; }

        // New properties
        public int prog_sessionCount { get; set; }
        public int prog_step { get; set; }
        public double prog_WeekdayPrice { get; set; }
        public double prog_WeekendPrice { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
