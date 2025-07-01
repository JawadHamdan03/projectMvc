using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;

namespace ProjectMVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        AppDbContext DB = new AppDbContext();
        public IActionResult Index()
        {
            return View(DB.categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            DB.categories.Add(category);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = DB.categories.FirstOrDefault(c => c.Id==id);
            DB.categories.Remove(category);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = DB.categories.FirstOrDefault(c => c.Id == id);
            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category) {
            DB.categories.Update(category);
            DB.SaveChanges();
        return RedirectToAction("Index");
        }
       
    }
}
