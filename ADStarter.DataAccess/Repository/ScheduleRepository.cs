using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private ApplicationDBContext _db;
        public ScheduleRepository(ApplicationDBContext db) : base(db) 
        { 
            _db = db;
        }
        public void Update(Schedule obj)
        {
            _db.Schedules.Update(obj);
        }

    }
}
