using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProgramRepository Program { get; }
        IParentRepository Parent { get; }
        IChildRepository Child { get; }
        ITreatmentHistoryRepository TreatmentHistory { get; }
        void Save();
    }
}
