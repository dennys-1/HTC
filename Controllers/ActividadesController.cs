using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hotel_santa_ursula_II.Models;
using hotel_santa_ursula_II.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace hotel_santa_ursula_II.Controllers
{
    public class ActividadesController : Controller
    {
        private readonly ILogger<ActividadesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ActividadesController(ILogger<ActividadesController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Mostrar()
        {
            var productos = from o in _context.actividades select o;
            productos = productos.Where(s => s.Estado.Equals("Disponible"));
            return View(await productos.ToListAsync());
        }
        public async Task<IActionResult> Detallesa(int? id)
        {
            Models.Actividades objProduct = await _context.actividades.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }
        [HttpGet]
        public async Task<IActionResult> Mostrar(String Empsearch){
            ViewData["Getemployeedetails"]=Empsearch;
            var empquery=from x in _context.actividades select x;
            if(!string.IsNullOrEmpty(Empsearch)){
                empquery=empquery.Where(x =>x.codigo.Contains(Empsearch))  ;
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }
        public async Task<IActionResult> Agregar(int? id)
        {
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Actividades> productos = new List<Actividades>();
                return  View("Mostrar",productos);
            }else{
                var producto = await _context.actividades.FindAsync(id);
                Carrito2 proforma = new Carrito2();
                proforma.Producto = producto;
                proforma.precio = producto.precio;
                proforma.codigo = producto.codigo;
                proforma.nombre = producto.nombre;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return  RedirectToAction(nameof(Mostrar));
            }
        }
    }
}