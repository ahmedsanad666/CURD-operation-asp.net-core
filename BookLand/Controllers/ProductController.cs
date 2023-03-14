using BookLand.DataAccess;
using BookLand.DataAccess.Repository;
using BookLand.DataAccess.Repository.IRepository;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;

namespace BookLand.Controllers
{
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;
        // for uplaoding  files 
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)

        {
            _UnitOfWork = UnitOfWork;
            _hostEnvironment = hostEnvironment;
        }


        // read or view data
        public IActionResult Index()
        {



            return View();
        }

        public IActionResult AllBooks()
        {
            IEnumerable<Product> BookList = _UnitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

            return View(BookList);
        }


        // update and delete  categories
        // with param id 
        public IActionResult UpSert(int? id)
        {


            // create form with all product valuse so get product model 
            Product product = new();
            // get cover and categoies 
            //SelectListItem for option tage and provid the text and val u need to option tag

            IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );
            IEnumerable<SelectListItem> CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );



            // if the id == null so create else update

            if (id == null || id == 0)
            {
                // create new book
                ViewBag.CategoryList = CategoryList;
                ViewData["CoverTypeList"] = CoverTypeList;

                return View(product);

            }
            else
            {
                // update 
                ViewBag.CategoryList = CategoryList;
                ViewData["CoverTypeList"] = CoverTypeList;
                var productList = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

                return View(productList);

            }


            return View(product);
        }






        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(Product obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // first handel img file and save img to wwwroot folder 
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string FileName = Guid.NewGuid().ToString();
                    // find the final location
                    var upload = Path.Combine(wwwRootPath, @"images/products");
                    var extention = Path.GetExtension(file.FileName);

                    if (obj.ImagUrl != null)
                    {
                        var oldPath = Path.Combine(wwwRootPath, obj.ImagUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(upload, FileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    };



                    obj.ImagUrl = @"\images\products\" + FileName + extention;

                }

                if (obj.Id == 0)
                {

                    _UnitOfWork.Product.Add(obj);
                } else
                {
                    _UnitOfWork.Product.Update(obj);
                }
                _UnitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        // get all data from database with api 
        public IActionResult GetAll()
        {
            var products = _UnitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = products });
        }

        // delet 
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "error getting the data" });
            }

            // delet obj image from folder 
            var oldPath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImagUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();

            return Json(new { success = true, message = "deleted successfully " });

        }
    }
}