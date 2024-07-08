using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class DashboardVM
    {
        public int NumberOfChildren { get; set; }
        public int NumberOfParent { get; set; }
        public int NumberOfTherapist { get; set; }
        public int NumberOfCustomerService { get; set; }
        public double TotalInvoiceAmount { get; set; }
        public double TotalPaidAmount { get; set; }
    }
}