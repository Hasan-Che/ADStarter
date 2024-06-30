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
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        private ApplicationDBContext _db;
        public AnnouncementRepository(ApplicationDBContext db) : base(db) 
        { 
            _db = db;
        }
        public void Update(Announcement obj)
        {
            _db.Announcements.Update(obj);
        }

    }
}
