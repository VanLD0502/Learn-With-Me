
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Categories.OrderByDescending(p => p.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == category.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh muc da co trong db");
                    return View(category);
                }

                _dataContext.Add(category);

                await _dataContext.SaveChangesAsync();

                TempData["success"] = "Them san pham thanh cong";

                return Redirect("Index");
            }
            else
            {
                TempData["error"] = "Model co vai thu bi loi";
                string errorMsg = "";
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorMsg = errorMsg + error.ErrorMessage + "\n";
                    }
                }

                return BadRequest(errorMsg);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryModel category)
        {
            Console.WriteLine("hello", category.Id);
            var existCategory = await _dataContext.Categories.FindAsync(category.Id);
            Console.WriteLine($"{id} {category.Id}, {existCategory.Name}");
            if (ModelState.IsValid)
            {
                existCategory.Slug = category.Name.Replace(" ", "-");

                existCategory.Name = category.Name;
                existCategory.Description = category.Description;
                existCategory.Status = category.Status;
                _dataContext.Update(existCategory);

                await _dataContext.SaveChangesAsync();

                TempData["success"] = "Cap nhat san pham thanh cong";

                return RedirectToAction("Index");
            }


            else
            {
                TempData["error"] = "Model co vai thu bi loi";
                string errorMsg = "";
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorMsg = errorMsg + error.ErrorMessage + "\n";
                    }
                }
                return BadRequest(errorMsg);
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);

            _dataContext.Categories.Remove(category);

            await _dataContext.SaveChangesAsync();

            TempData["error"] = "San pham da xoa";

            return RedirectToAction("Index");
        }



    }
}