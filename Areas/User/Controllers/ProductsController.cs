using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;

namespace ProjectMVC.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        AppDbContext DB = new AppDbContext();
        public IActionResult Index()
        {
            return View(DB.Products.Include(p=>p.Category).ToList());
        }
    }
}
