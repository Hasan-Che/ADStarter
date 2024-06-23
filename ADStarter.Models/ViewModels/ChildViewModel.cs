using System;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models.ViewModels
{
    public class ChildViewModel
    {
        [Required]
        [StringLength(100)]
        public string c_myKid { get; set; }

        public string parent_ID { get; set; }

        [Required]
        public int prog_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string c_name { get; set; }

        [Required]
        public int c_age { get; set; }

        [Required]
        [StringLength(10)]
        public string c_gender { get; set; }

        [Required]
        public DateTime c_dob { get; set; }

        [Required]
        [StringLength(50)]
        public string c_nationality { get; set; }

        [Required]
        [StringLength(50)]
        public string c_religion { get; set; }

        [Required]
        [StringLength(50)]
        public string c_race { get; set; }

        [Required]
        [StringLength(50)]
        public string c_status { get; set; }
        [Required]
        [StringLength(50)]
        public string c_photo { get; set; }

    }
}
