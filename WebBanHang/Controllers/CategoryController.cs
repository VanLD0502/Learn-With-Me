using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

namespace MyApp.Namespace
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index(string Slug = "")
        {
            CategoryModel category = _context.Categories.Where(c => c.Slug == Slug).FirstOrDefault();

            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var productsByCategoty = _context.Products.Where(p => p.CategoryId == category.Id);

            return View(await productsByCategoty.OrderByDescending(p => p.Id).ToListAsync());
        }

    }
}
