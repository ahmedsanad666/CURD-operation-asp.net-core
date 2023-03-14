using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // lets assum T is class or category c  lass then i need 1.get all cat 2.Create 4.edit  3.update 4.remove


        //edit 
        T GetFirstOrDefault(Expression<Func<T, bool>> filter,string ? includeProperties = null );
        //get all with IEnumrable<cat>


        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
