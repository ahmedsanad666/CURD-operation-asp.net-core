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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            var ProductFromDb = _db.Products.FirstOrDefault(ui => ui.Id == obj.Id);

            if (ProductFromDb != null)
            {
                ProductFromDb.Name = obj.Name;
                ProductFromDb.ISBN = obj.ISBN;
                ProductFromDb.Author = obj.Author;
                ProductFromDb.Description = obj.Description;
                ProductFromDb.PriceList = obj.PriceList;
                ProductFromDb.Price = obj.Price;
                ProductFromDb.Price50 = obj.Price50;
                ProductFromDb.Price100 = obj.Price100;
                ProductFromDb.CategoryId = obj.CategoryId;
                ProductFromDb.CoverTypeId = obj.CoverTypeId;
                if (obj.ImagUrl != null)
                {
                    ProductFromDb.ImagUrl = obj.ImagUrl;
                }
            }

        }
    }
}
