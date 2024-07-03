using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
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
        public IParentRepository Parent { get; private set; }
        public IScheduleRepository Schedule { get; private set; }
        public IChildRepository Child { get; private set; }
        public ISlotRepository Slot { get; private set; } 
        public ITreatmentHistoryRepository TreatmentHistory { get; private set; }
        public IInvoiceRepository Invoice { get; private set; }
        public IReportRepository Report { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Program = new ProgramRepository(_db);
            Therapist = new TherapistRepository(_db);
            CustomerService = new CustomerServiceRepository(_db);
            Admin = new AdminRepository(_db);
            Announcement = new AnnouncementRepository(_db);
            Parent = new ParentRepository(_db);
            Slot = new SlotRepository(_db);
            Schedule = new ScheduleRepository(_db);
            Child = new ChildRepository(_db);
            Therapist = new TherapistRepository(_db);
            TreatmentHistory = new TreatmentHistoryRepository(_db);
            Invoice = new InvoiceRepository(_db);
            Report = new ReportRepository(_db);
            Payment = new PaymentRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges(); 
        }


        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}