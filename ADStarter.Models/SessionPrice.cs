#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADStarter.Models
{
    public partial class SessionPrice
    {
        [Key]
        public int session_ID { get; set; }

        public int session_bilangan { get; set; } // Add this line
        public string session_day { get; set; }

        [StringLength(100)]
        public string session_name { get; set; }


        public double sp_price { get; set; }

        public int prog_ID { get; set; }


        [ForeignKey(nameof(prog_ID))]
        public virtual Program Program { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
