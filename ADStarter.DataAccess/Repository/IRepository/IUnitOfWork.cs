using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProgramRepository Program { get; }
        IParentRepository Parent { get; }
        IScheduleRepository Schedule { get; }
        IChildRepository Child { get; }
        ISlotRepository Slot { get; }
        ITherapistRepository Therapist { get; }
        ITreatmentHistoryRepository TreatmentHistory { get; }
        IInvoiceRepository Invoice { get; }
        IReportRepository Report { get; }
        void Save();
    }
}
