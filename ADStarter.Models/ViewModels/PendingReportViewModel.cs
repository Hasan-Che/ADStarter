using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class PendingReportViewModel
    {
        public int ScheduleId { get; set; }
        public string ChildName { get; set; }
        public DateTime ReportDateTime { get; set; }
        public string ProgramName { get; set; }
        public string SlotTime { get; set; }
    }
}
