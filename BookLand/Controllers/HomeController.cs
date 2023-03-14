using BookLand.DataAccess.Repository.IRepository;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLand.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;

       

        public HomeController(ILogger<HomeController> logger, IUnitOfWork UnitOfWork)
        {
            _logger = logger;
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<Product> ProductList = _UnitOfWork.Product.GetAll(includeProperties: "Category");

            ProductList = ProductList.Take(12);

            return View(ProductList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}