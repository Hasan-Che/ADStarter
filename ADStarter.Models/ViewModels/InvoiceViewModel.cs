namespace ADStarter.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Parent> Parents { get; set; }
    }
}