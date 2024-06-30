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
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        private ApplicationDBContext _db;
        public ParentRepository(ApplicationDBContext db) : base(db) 
        { 
            _db = db;
        }
        public void Update(Parent obj)
        {
            _db.Parents.Update(obj);
        }

    }
}
