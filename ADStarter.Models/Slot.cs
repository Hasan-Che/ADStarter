#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models
{
    public partial class Slot
    {
        [Key]
        public int slot_ID { get; set; }

        public string slot_time { get; set; } // Change data type to string

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
