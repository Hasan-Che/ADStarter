using System;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models.ViewModels
{
    public class ChildViewModel
    {
        public int c_myKid { get; set; }
        public int prog_ID { get; set; }
        public int parent_ID { get; set; }
        public string c_name { get; set; }
        public int c_age { get; set; }
        public string c_gender { get; set; }
        public DateTime c_dob { get; set; }
        public string c_nationality { get; set; }
        public string c_religion { get; set; }
        public string c_race { get; set; }
        public string c_status { get; set; }
        public string c_photo { get; set; } // Add this property
                                            // Add other properties as needed
    }

}
