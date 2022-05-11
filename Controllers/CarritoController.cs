using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using Rotativa.AspNetCore;

namespace hotel_santa_ursula_II.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CarritoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
               // GET: Proforma
        public async Task<IActionResult> Index3()
        {
            var userID = _userManager.GetUserName(User);
            var carrito = from o in _context.DataProforma select o;
            carrito = carrito.
                Include(p => p.habitacion).
                Where(s => s.UserID.Equals(userID));

                //conversion de fechas a numeros e integer
                //--------------------------------------------
            
            return View(await carrito.ToListAsync());
        }
    }
}
