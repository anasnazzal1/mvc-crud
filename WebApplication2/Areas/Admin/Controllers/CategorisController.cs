using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategorisController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var catgores = context.Categories.ToList();
            ViewBag.Catgores = catgores;
            return View();
        }

        public IActionResult Delete(int id) {
            var cat = context.Categories.Find(id);
            if (cat != null) { 
                context.Categories.Remove(cat);
                context.SaveChanges();
            
            }
            return RedirectToAction("Index");
        
        
        }
     
        public IActionResult Create()
        {
            return View(new Category());
        }

        // POST: Admin/Categoris/Create
      
       
        public IActionResult Store(Category request)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",request);

            }
            context.Categories.Add(request);
            context.SaveChanges();
            return RedirectToAction("Index");
          
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = context.Categories.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id,Name,Description")] Category re)
        {
            context.Categories.Update(re);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
