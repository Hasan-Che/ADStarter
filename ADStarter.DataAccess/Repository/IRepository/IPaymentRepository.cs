using ADStarter.Models;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        double GetTotalPaidAmount();
    }
}