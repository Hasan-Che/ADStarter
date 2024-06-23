
﻿//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;


//namespace ADStarter.Models.ViewModels
//{
//    public class ScheduleViewModel
//    {

//        [Required(ErrorMessage = "Child is required.")]
//        public int Child_ID { get; set; }

//        [Required(ErrorMessage = "Session Date is required.")]
//        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
//        public DateTime SessionDate { get; set; }

//        [Required(ErrorMessage = "Slot is required.")]
//        public int Slot_ID { get; set; }

//        public int Session_ID { get; set; }

//        // Optional properties

//        public string ChildName { get; set; }
//        public string SlotTime { get; set; }
//        public string ProgramName { get; set; }
//        public string TherapistName { get; set; }
//    }

//    public class ChildScheduleViewModel
//    {
//        public int Child_ID { get; set; }
//        public string ChildName { get; set; }
//        public List<ScheduleViewModel> Schedules { get; set; }
//    }
//}
