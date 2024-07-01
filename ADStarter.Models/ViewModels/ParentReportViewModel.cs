using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class ParentReportViewModel
    {
        public int ScheduleId { get; set; }
        public string ChildName { get; set; }
        public DateTime SessionDateTime { get; set; }
        public string ProgramName { get; set; }
        public string SlotTime { get; set; }

        public string ReportFile { get; set; }
        public string ReportRemark { get; set; }
        public string ReportStatus { get; set; }
    }
} 
