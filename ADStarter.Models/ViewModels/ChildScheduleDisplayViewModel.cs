using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class ChildScheduleDisplayViewModel
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; }
    }
}
