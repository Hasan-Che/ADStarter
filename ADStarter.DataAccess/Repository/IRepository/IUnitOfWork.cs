using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ITherapistRepository Therapist { get; }
        IAdminRepository Admin { get; }
        IProgramRepository Program { get; }
        ICustomerServiceRepository CustomerService { get; }
        IAnnouncementRepository Announcement { get; }
        IParentRepository Parent { get; }
        IScheduleRepository Schedule { get; }
        IChildRepository Child { get; }
        ISlotRepository Slot { get; }
        ITreatmentHistoryRepository TreatmentHistory { get; }
        IInvoiceRepository Invoice { get; }
        IReportRepository Report { get; }
        IPaymentRepository Payment { get; }
        void Save();
        IDbContextTransaction BeginTransaction();
    }
}
