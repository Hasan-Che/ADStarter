#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ADStarter.Models
{
    public partial class Parent
    {
        [Key]
        public int parent_ID { get; set; }

        [Required(ErrorMessage = "The IC number is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Invalid MyKid number. Must be exactly 12 digits.")]
        [StringLength(100)]
        public string f_ID { get; set; }

        [Required(ErrorMessage = "The IC number is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Invalid MyKid number. Must be exactly 12 digits.")]
        [StringLength(100)]
        public string m_ID { get; set; }

        [StringLength(100)]
        public string f_name { get; set; }

        [StringLength(15)]
        public string f_phoneNum { get; set; }

        [StringLength(50)]
        public string f_race { get; set; }

        [StringLength(255)]
        public string f_address { get; set; }

        [StringLength(255)]
        public string f_Waddress { get; set; }

        [StringLength(100)]
        public string f_email { get; set; }

        [StringLength(100)]
        public string f_occupation { get; set; }

        [StringLength(50)]
        public string f_status { get; set; }

        [StringLength(100)]
        public string m_name { get; set; }

        [StringLength(15)]
        public string m_phoneNum { get; set; }

        [StringLength(50)]
        public string m_race { get; set; }

        [StringLength(255)]
        public string m_address { get; set; }

        [StringLength(255)]
        public string m_Waddress { get; set; }

        [StringLength(100)]
        public string m_email { get; set; }

        [StringLength(50)]
        public string m_status { get; set; }

        public double fm_income { get; set; }


        public string UserId { get; set; }

        public virtual ICollection<Child>? Children { get; set; } = new List<Child>();
    }
}
