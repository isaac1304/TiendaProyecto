using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TiendaProyecto.Models;
using Microsoft.EntityFrameworkCore;
using TiendaProyecto.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TiendaProyecto.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = new SelectList(await _context.Categorias.ToListAsync(), "idCategoria", "nombre");
            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Category")] Product product, IFormFile imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.Length > 0)
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageUpload.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageUpload.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/images/" + imageUpload.FileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = new SelectList(await _context.Categorias.ToListAsync(), "idCategoria", "nombre", product.Category);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryList = new SelectList(await _context.Categorias.ToListAsync(), "idCategoria", "nombre", product.Category);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Category,ImageUrl")] Product product, IFormFile imageUpload)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageUpload != null && imageUpload.Length > 0)
                    {
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageUpload.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await imageUpload.CopyToAsync(stream);
                        }
                        product.ImageUrl = "/images/" + imageUpload.FileName;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = new SelectList(await _context.Categorias.ToListAsync(), "idCategoria", "nombre", product.Category);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}