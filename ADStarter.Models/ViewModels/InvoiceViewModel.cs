namespace ADStarter.Models
{
    public class InvoiceViewModel
{
    public int Invoice_ID { get; set; }
    public double Invoice_Amount { get; set; }
    public string C_MyKid { get; set; }
    
    public string ChildName { get; set; }
    public DateTime Due_Date { get; set; }
    public string ProgramName { get; set; }
    public DateTime Session_Date { get; set; }
    public int Schedule_ID { get; set; }
    public List<Payment> Payments { get; set; }
    public Schedule Schedule { get; set; } 
    public IEnumerable<Child> Children { get; set; }
    public bool IsPaid { get; set; }
}
}

