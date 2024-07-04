namespace ADStarter.Models.ViewModels
{
    public class ChildScheduleViewModel
    {
     
        public string TherapistName { get; set; }
        public string ChildName { get; set; }
        public DateTime SessionDate { get; set; }
        public string SlotTime { get; set; }
        public string ProgramName { get; set; }
        public int ScheduleId { get; set; } // Include ScheduleId property
    }
}
