using System;
using System.ComponentModel.DataAnnotations;

namespace ADStarter.Models.ViewModels
{
    public class NewScheduleViewModel
    {
        [Required(ErrorMessage = "Child is required.")]
        public int Child_ID { get; set; }

        [Required(ErrorMessage = "Session Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = "Slot is required.")]
        public int Slot_ID { get; set; }
        public double SlotPrice { get; set; }
        public int Session_ID { get; set; }
    }
}
