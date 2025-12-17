
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;

        public BrandController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = brand.Name.Replace(" ", "-");
                var slug = await _dataContext.Brands.FirstOrDefaultAsync(c => c.Slug == brand.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh muc da co trong db");
                    return View(brand);
                }

                _dataContext.Add(brand);

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
            BrandModel category = await _dataContext.Brands.FindAsync(Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandModel brand)
        {
            var existBrand = await _dataContext.Brands.FindAsync(brand.Id);
            if (ModelState.IsValid)
            {
                existBrand.Slug = brand.Name.Replace(" ", "-");

                existBrand.Description = brand.Description;
                existBrand.Status = brand.Status;
                _dataContext.Update(existBrand);

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
            BrandModel brand = await _dataContext.Brands.FindAsync(Id);

            _dataContext.Brands.Remove(brand);

            await _dataContext.SaveChangesAsync();

            TempData["error"] = "San pham da xoa";

            return RedirectToAction("Index");
        }



    }
}