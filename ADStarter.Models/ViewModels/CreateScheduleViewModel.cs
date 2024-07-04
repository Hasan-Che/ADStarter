namespace ADStarter.Models.ViewModels
{
    public class CreateScheduleViewModel
    {
        public int t_ID { get; set; }
        public int ProgramId { get; set; }

        public string ChildId { get; set; }

        public List<DateTime> SelectedDates { get; set; } = new List<DateTime>();
        public List<int> SlotIds { get; set; } = new List<int>(); // 
        public int ProgSessionCount { get; set; } // Add th
      

        // Additional properties as needed

        public List<SlotViewModel> AvailableSlots { get; set; }
    }
}
