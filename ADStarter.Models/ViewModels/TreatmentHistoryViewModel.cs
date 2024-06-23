using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models.ViewModels
{
    public class TreatmentHistoryViewModel
    {
        [StringLength(100)]
        public string th_pediatrician { get; set; }

        [StringLength(255)]
        public string th_recommendation { get; set; }

        [Required]
        public DateTime th_deadline { get; set; }

        [StringLength(255)]
        public string th_diagnosis { get; set; }

        [StringLength(255)]
        public string th_prevTherapy { get; set; }
    }
}
