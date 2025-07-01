using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;

namespace ProjectMVC.Areas.User.Controllers
{
    [Area("User")]
    public class CategoriesController : Controller
    {
        AppDbContext DB = new AppDbContext();
        public IActionResult Index()
        {
            return View(DB.categories.ToList());
        }
    }
}
