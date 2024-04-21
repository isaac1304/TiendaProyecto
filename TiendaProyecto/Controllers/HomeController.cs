
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TiendaProyecto.Models;
using Microsoft.EntityFrameworkCore;
using TiendaProyecto.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Diagnostics;

namespace TiendaProyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Func<Carrito> _obtenerCarrito;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Carrito ObtenerCarrito()
        {
            var carritoJson = _httpContextAccessor.HttpContext.Session.GetString("Carrito");
            var carrito = carritoJson != null ? System.Text.Json.JsonSerializer.Deserialize<Carrito>(carritoJson) : new Carrito();
            return carrito;
        }

        public void GuardarCarrito(Carrito carrito)
        {
            var carritoJson = System.Text.Json.JsonSerializer.Serialize(carrito);
            _httpContextAccessor.HttpContext.Session.SetString("Carrito", carritoJson);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            var carrito = ObtenerCarrito();
            var producto = await _context.Products.FindAsync(productId);

            if (producto == null)
            {
                return NotFound();
            }

            var itemCarrito = carrito.Items.FirstOrDefault(i => i.Product.Id == productId);

            if (itemCarrito != null)
            {
                itemCarrito.Cantidad++;
            }
            else
            {
                carrito.Items.Add(new ItemCarrito
                {
                    Precio = producto.Price,
                    Product = producto,
                    Cantidad = 1
                });
            }

            GuardarCarrito(carrito); // Assuming you have this method from the CarritoController

            return RedirectToAction("Index", "Home"); // Redirect back to the home page
        }
    }
}
