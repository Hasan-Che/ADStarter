using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
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
        public IChildRepository Child { get; private set; }
        public ITreatmentHistoryRepository TreatmentHistory { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Program = new ProgramRepository(_db);
            Parent = new ParentRepository(_db);
            Child = new ChildRepository(_db);
            TreatmentHistory = new TreatmentHistoryRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges(); 
        }
    }
}
