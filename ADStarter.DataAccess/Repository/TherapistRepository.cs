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
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        private ApplicationDBContext _db;
        public TherapistRepository(ApplicationDBContext db) : base(db) 
        { 
            _db = db;
        }
        public void Update(Therapist obj)
        {
            _db.Therapists.Update(obj);
        }

    }
}
