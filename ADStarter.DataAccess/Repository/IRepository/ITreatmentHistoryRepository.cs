using ADStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface ITreatmentHistoryRepository : IRepository<TreatmentHistory>
    {
        void Add(TreatmentHistory obj);
        void Update(TreatmentHistory obj);
    }
}
