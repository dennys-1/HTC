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
    public class ProformaActividadesController : Controller
    {
         private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProformaActividadesController(ApplicationDbContext context,
         UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Principal()
        {
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma2 select o;
            items = items.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(userID));

                return View(await items.ToListAsync());
        }
    }
}