using BookLand.DataAccess.Repository.IRepository;
using BookLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.DataAccess.Repository
{
    // : after calss name means the interface or otherclass that would be implemented
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
            
        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
