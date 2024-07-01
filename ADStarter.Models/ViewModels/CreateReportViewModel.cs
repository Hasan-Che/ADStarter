using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models.ViewModels
{
    public class CreateReportViewModel
    {
        public int ReportId { get; set; } // Include this property for updating existing reports

        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "Report title is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string ReportTitle { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Report Date")]
        public DateTime ReportDate { get; set; } // Changed property name to ReportDate to avoid ambiguity

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
        public string ReportRemark { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile ReportFile { get; set; }

        // Additional properties if needed for display purposes
        public string ChildName { get; set; }

        public string ProgramName { get; set; }

        public string SlotTime { get; set; }
    }
}
