using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class TherapistScheduleViewModel
    {
        public string ChildName { get; set; }
        public DateTime SessionDateTime { get; set; }
        public string SlotTime { get; set; }
        public string ProgramName { get; set; }
        public double SlotPrice { get; set; }
    }
}
