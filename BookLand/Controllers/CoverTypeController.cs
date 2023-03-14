    using BookLand.DataAccess.Repository.IRepository;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BookLand.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CoverTypeController(IUnitOfWork UnitOfWork)

        {
            _UnitOfWork = UnitOfWork;
        }


        // read or view data
        public IActionResult Index()
        {

            IEnumerable<CoverType> CoverList = _UnitOfWork.CoverType.GetAll();

           
            return View(CoverList);
        }

        // Add new or create new cover page 
        //get
        public IActionResult CreateCover()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCover(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(obj);
                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);


        }
        // update 
        //get 
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(ui => ui.Id == id);
            if(obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }


        // post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Update(obj);
                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }
       
        

        // delete

        //get req
        public IActionResult Delete(int ? id)
        {
            if(id == null)
            {
                return NotFound();

            }
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(ui => ui.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // post req 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCover(int ? id)
        {
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(ui => ui.Id == id);

            if(obj == null)
            {
                return NotFound();
            }


            _UnitOfWork.CoverType.Remove(obj);
            _UnitOfWork.Save();
                
            return RedirectToAction("Index");
        }
    }
}
