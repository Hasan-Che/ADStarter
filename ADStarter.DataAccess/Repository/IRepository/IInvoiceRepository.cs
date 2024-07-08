using ADStarter.Models;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        double GetTotalInvoiceAmount();
    }
}