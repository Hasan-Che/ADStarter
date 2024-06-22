namespace ADStarter.Models.ViewModels
{
    public class ChildScheduleDisplayViewModel
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; }
    }

}
