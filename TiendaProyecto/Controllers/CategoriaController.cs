using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaProyecto.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Categoria> Categoria = _context.Categorias.ToList();
            return View(Categoria);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("idTipoHabitacion,nombre,descripcion")] Categoria Categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Categoria);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var Categoria = await _context.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }
            return View(Categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("idCategoria,nombre,descripcion")] Categoria Categoria)
        {
            if (id != Categoria.idCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(Categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Categoria);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var Categoria = await _context.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }
            return View(Categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.idCategoria == id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}