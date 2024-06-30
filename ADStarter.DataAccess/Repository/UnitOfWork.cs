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
        public ITherapistRepository Therapist { get; private set; }
        public ICustomerServiceRepository CustomerService { get; private set; }
        public IAdminRepository Admin { get; private set; }
        public IAnnouncementRepository Announcement { get; private set; }

        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Program = new ProgramRepository(_db);
            Therapist = new TherapistRepository(_db);
            CustomerService = new CustomerServiceRepository(_db);
            Admin = new AdminRepository(_db);
            Announcement = new AnnouncementRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges(); 
        }
    }
}
