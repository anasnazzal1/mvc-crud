using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdactsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var prodact = context.prodacts.ToList();
            var PViewMpdel = new List<ProdactsViewModel>();
            var prodacts = context.prodacts.Join(context.Categories, p => p.CategoryId, c => c.Id, (p, c) => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Status,
                p.Image,
                catgruName = c.Name

            });
            foreach(var item in prodacts)
            {
                var vm = new ProdactsViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Status = item.Status,
                    Image = item.Image,
                    CategoryName = item.catgruName,

                };
                PViewMpdel.Add(vm); 
            }




            ViewBag.Prodacts = PViewMpdel;
            return View();
        }
        public IActionResult delete(int id)
        {
            var item = context.prodacts.Find(id);
            if (item != null)
            {
                context.prodacts.Remove(item);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Image", item.Image);
                System.IO.File.Delete(filepath);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Create() {
            
        var prod = context.Categories.ToList();
            ViewBag.Categories = prod;
        return View(new Prodacts());
     
        }

        public IActionResult Store(Prodacts prodacts,IFormFile file) {



            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString(); //للعمل اسم للصورة
                fileName += Path.GetFileName(file.FileName);// يضيف اسم الصورة الاساسي
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Image", fileName);

                using (var strem = System.IO.File.Create(filepath)) { 
                
                file.CopyTo(strem);
                }
                prodacts.Image = fileName;
                context.prodacts.Add(prodacts);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        
        
        
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var proda = context.prodacts.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(proda);
        }
        [HttpPost]
        public IActionResult Edit(Prodacts prod, IFormFile? file)
        {
            var product = context.prodacts.Find(prod.Id);
           

            // ✅ إذا تم رفع ملف جديد
            if (file != null && file.Length > 0)
            {
                // حذف الصورة القديمة إن وجدت
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", product.Image);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // حفظ الصورة الجديدة
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                product.Image = fileName;
            }

            // ✅ تحديث باقي الحقول
            product.Name = prod.Name;
            product.Description = prod.Description;
            product.CategoryId = prod.CategoryId;
            product.Status = prod.Status;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
