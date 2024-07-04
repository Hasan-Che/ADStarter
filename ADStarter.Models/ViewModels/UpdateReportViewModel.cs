using Microsoft.AspNetCore.Http;

namespace ADStarter.Models.ViewModels
{
    public class UpdateReportViewModel
    {
        public int ReportId { get; set; }
        public int ScheduleId { get; set; }
        public string ReportTitle { get; set; }
        public DateTime ReportDateTime { get; set; }
        public string ReportRemark { get; set; }
        public IFormFile ReportFile { get; set; }
    }
}
