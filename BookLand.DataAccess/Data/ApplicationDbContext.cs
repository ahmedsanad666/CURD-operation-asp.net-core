using BookLand.Models;
using Microsoft.EntityFrameworkCore;


// the location of this folder
namespace BookLand.DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        //passing the options and sending it to base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

         

        }
            // create categores table in database
            public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }    
        public DbSet<Product> Products { get; set; }
    }
}
