using BookLand.Models;

namespace BookLand.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {

        void Update(CoverType obj);
    }
}