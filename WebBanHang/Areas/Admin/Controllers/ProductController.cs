using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // GET: ProductController

        private readonly DataContext _context;

        private readonly IWebHostEnvironment _env;
        public ProductController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
        }

        public async Task<ActionResult> Details(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");

            var productById = _context.Products.Where(p => p.Id == Id).FirstOrDefault();

            return View(productById);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "San pham da co trong db");
                    return View(product);
                }
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_env.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);

                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    product.Image = imageName;
                }

                _context.Add(product);

                await _context.SaveChangesAsync();

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
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product = await _context.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, ProductModel product)
        {
            Console.WriteLine("Edit " + product.Image);
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

            var existProduct = await _context.Products.FindAsync(product.Id);
            if (ModelState.IsValid)
            {
                existProduct.Slug = product.Name.Replace(" ", "-");
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_env.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);

                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();



                    string oldFilePath = Path.Combine(uploadDir, existProduct.Image);
                    try
                    {
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Hello world");
                    }

                    existProduct.Image = imageName;

                }


                existProduct.Name = product.Name;
                existProduct.Description = product.Description;
                existProduct.Price = product.Price;
                existProduct.CategoryId = product.CategoryId;
                existProduct.BrandId = product.BrandId;
                _context.Update(existProduct);

                await _context.SaveChangesAsync();

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
            ProductModel product = await _context.Products.FindAsync(Id);

            if (!string.Equals("noname.jpg", product.Image))
            {
                string uploadDir = Path.Combine(_env.WebRootPath, "media/products");
                string oldFilePath = Path.Combine(uploadDir, product.Image);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }


            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            TempData["error"] = "San pham da xoa";

            return RedirectToAction("Index");
        }
    }
}
