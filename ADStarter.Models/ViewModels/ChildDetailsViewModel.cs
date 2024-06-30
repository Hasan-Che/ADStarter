using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class ChildDetailsViewModel
    {
        public string ChildName { get; set; }
        public int? Step { get; set; }
        public string TherapistName { get; set; }
        public string ChildId { get; set; }
       
        public int NewStep { get; set; }


        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Race { get; set; }
        public string ParentName { get; set; }
    }
}
