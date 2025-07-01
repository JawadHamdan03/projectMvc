using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;

namespace ProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        AppDbContext DB = new AppDbContext();
        public IActionResult Index()
        {
            return View(DB.Products.Include(p=> p.Category).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.categories = DB.categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); 

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                     ImageFile.CopyTo(stream);
                

                
                product.ImageUrl = "/images/" + fileName;
            }

            DB.Products.Add(product);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = DB.Products.FirstOrDefault(p => p.Id == id);
            
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                         System.IO.File.Delete(imagePath);

            }
            DB.Products.Remove(product);
            DB.SaveChanges();

            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public IActionResult Edit(int id )
        {
            var product = DB.Products.FirstOrDefault(p => p.Id == id);
           
            ViewBag.categories = DB.categories.ToList();
            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product newProduct , IFormFile Image)
        {
            var product = DB.Products.FirstOrDefault(p => p.Id == newProduct.Id);
           
           
                
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.CategoryId = newProduct.CategoryId;

               
                if (Image != null && Image.Length > 0)
                {
                    
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                         Image.CopyTo(stream);

                    product.ImageUrl = "/images/" + fileName;
                }

                DB.SaveChanges();
                return RedirectToAction("Index");
        }



        }

    }
