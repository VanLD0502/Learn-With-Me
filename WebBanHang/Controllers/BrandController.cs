

using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

namespace WebBanHang.Controllers
{
    public class BrandController : Controller
    {
        public DataContext _context;

        public BrandController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string Slug = "")
        {
            BrandModel brand = _context.Brands.Where(b => b.Slug == Slug).FirstOrDefault();

            if (brand == null)
            {
                Console.WriteLine("NULL");
                return RedirectToAction("Index");
            }

            var products = _context.Products.Where(p => p.BrandId == brand.Id);

            return View(await products.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}