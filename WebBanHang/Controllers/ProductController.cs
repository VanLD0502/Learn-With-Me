using Microsoft.AspNetCore.Mvc;
using WebBanHang.Repository;

namespace MyApp.Namespace
{
    public class ProductController : Controller
    {
        // GET: ProductController

        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");

            var productById = _context.Products.Where(p => p.Id == Id).FirstOrDefault();

            return View(productById);
        }

    }
}
