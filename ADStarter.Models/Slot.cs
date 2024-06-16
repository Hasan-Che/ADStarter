#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models
{
    public partial class Slot
    {
        [Key]
        public int slot_ID { get; set; }

        public TimeSpan slot_time { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
