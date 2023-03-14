using BookLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        /// by implementing Irepo here u have all methods in Irepo and so u can add ur own custorm funcs like update

        void Update(Product obj);
   
    }


}
