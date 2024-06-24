using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        public IProgramRepository Program { get; private set; }
        public IParentRepository Parent { get; private set; }
        public IScheduleRepository Schedule { get; private set; }
        public IChildRepository Child { get; private set; }
        public ISlotRepository Slot { get; private set; }
        public ITherapistRepository Therapist { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Program = new ProgramRepository(_db);
            Parent = new ParentRepository(_db);
            Slot = new SlotRepository(_db);
            Schedule = new ScheduleRepository(_db);
            Child = new ChildRepository(_db);
            Therapist = new TherapistRepository(_db);


        }
        

        public void Save()
        {
            _db.SaveChanges(); 
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
