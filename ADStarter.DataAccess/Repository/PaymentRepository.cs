using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADStarter.DataAccess.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDBContext _db;

        public PaymentRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public double GetTotalPaidAmount()
        {
            return _db.Payments.Sum(i => i.pay_amount);
        }
    }
}