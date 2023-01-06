using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
            
            return View(categories);
        }
        //Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category name and Display order can not be same!");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category Not Valid";
                return View(category);
            }

        }

        //Get Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
               return NotFound();
            }
            var category = _db.Categories.SingleOrDefault(category=>category.Id==id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category name and Display order can not be same!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();

                TempData["success"] = "Category Updated Successfully";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category Update Failed";
                return View(category);
            }

        }


        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id==0 || id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.SingleOrDefault(category => category.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //Post Delete
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = "Category Delete Failed";
                return NotFound();
            }
            var deleteCategory = _db.Categories.Find(id);
            if(deleteCategory== null)
            {
                TempData["error"] = "Category Delete Failed";
                return NotFound();
            }
            _db.Categories.Remove(deleteCategory);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }



    }
   
    
}
