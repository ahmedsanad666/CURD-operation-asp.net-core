using BookLand.DataAccess;
using BookLand.DataAccess.Repository;
using BookLand.DataAccess.Repository.IRepository;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
    
namespace BookLand.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;
        

        public CategoryController(IUnitOfWork  UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
           
        }


        // read or view data
        public IActionResult Index()
        {
           

            IEnumerable<Category> CategoryList = _UnitOfWork.Category.GetAll();
            return View(CategoryList); 
        }

        // create new category view

        //Get 
        public IActionResult Create()
        {
            return View();
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)

        {
            if (ModelState.IsValid)
            {
               
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category has been Created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Edit categories
        // with param id 
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryData = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id ==id);

            if (categoryData == null)
            {
                return NotFound();
            }

            return View(categoryData);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
				TempData["success"] = "Category has been Edited successfully";

				return RedirectToAction("Index");
            }
            return View(obj);


        }
         //delete

        public IActionResult Delete(int ? id)
        {
			if (id == null || id == 0)
			{
				return NotFound();
			}
            var categoryData = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
		
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (categoryData == null)
			{
				return NotFound();
			}

			return View(categoryData);
		}

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int ? id)
        {
            var obj = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
			{
				return NotFound();
			}

            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
		TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}

    }
}
