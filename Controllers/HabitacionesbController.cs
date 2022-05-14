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
    public class HabitacionesbController : Controller
    {
        private readonly ILogger<HabitacionesbController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HabitacionesbController(ILogger<HabitacionesbController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Indexhab()
        {
            return View();
        }
        public IActionResult Indexhab2()
        {
            return View();
        }
        public async Task<IActionResult> Mostrarhab()
        {
            var productos = from o in _context.habitaciones select o;
            productos = productos.Where(s => s.Status.Equals("Disponible"));
            return View(await productos.ToListAsync());
        }
        public async Task<IActionResult> Detalleshab(int? id)
        {
            Models.Habitaciones objProduct = await _context.habitaciones.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }
        
        [HttpGet]
        public async Task<IActionResult> Mostrarhab(String Empsearch){
            ViewData["Getemployeedetails"]=Empsearch;
            var empquery=from x in _context.habitaciones select x;
            if(!string.IsNullOrEmpty(Empsearch)){
                empquery=empquery.Where(x =>x.numero.Contains(Empsearch))  ;
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }
        public async Task<IActionResult> Agregar2(int? id)
        {
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar una actividad";
                List<Habitaciones> productos = new List<Habitaciones>();
                return  View("Mostrarhab",productos);
            }else{
                var producto = await _context.habitaciones.FindAsync(id);
                Carritoh proforma = new Carritoh();
                proforma.Prodhab = producto;
                proforma.price = producto.price;
                proforma.numero = producto.numero;
                proforma.Nomhab = producto.Nomhab;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return  RedirectToAction(nameof(Mostrarhab));
            }
        }
        
        
        
    }
}