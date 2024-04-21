using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TiendaProyecto.Models;
using Microsoft.EntityFrameworkCore;
using TiendaProyecto.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TiendaProyecto.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly HomeController _homeController;

        public CarritoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, HomeController homeController)
        {
            _homeController = homeController;
        }

        public IActionResult Index()
        {
            var carrito = _homeController.ObtenerCarrito();
            return View(carrito.Items);
        }

        public IActionResult EliminarDelCarrito(int productoId)
        {
            var carrito = _homeController.ObtenerCarrito();
            var itemCarrito = carrito.Items.FirstOrDefault(i => i.Product.Id == productoId);
            if (itemCarrito != null)
            {
                carrito.Items.Remove(itemCarrito);
                _homeController.GuardarCarrito(carrito);
            }
            return RedirectToAction("Index", "Carrito");
        }

        public IActionResult VaciarCarrito()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Carrito");
            return RedirectToAction("Index", "Carrito");
        }
    }
}