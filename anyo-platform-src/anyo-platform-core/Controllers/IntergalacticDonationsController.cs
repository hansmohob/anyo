using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using anyo_platform.Models;
using anyo_platform_core.Data;
using System.Security.Claims;

namespace anyo_platform_core.Controllers
{
    public class IntergalacticDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IntergalacticDonation.Include(i => i.Donor).Include(i => i.Package);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IntergalacticDonation == null)
            {
                return NotFound();
            }

            var intergalacticDonation = await _context.IntergalacticDonation
                .Include(i => i.Donor)
                .Include(i => i.Package)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticDonation == null)
            {
                return NotFound();
            }

            return View(intergalacticDonation);
        }

        public IActionResult Create(int? id)
        {
            ViewData["DonorId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["MissionId"] = id;
            ViewData["PackageId"] = new SelectList(_context.IntergalacticPackages, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,Quantity,MissionId,DonorId,PackageId")] IntergalacticDonation intergalacticDonation)
        {
            if (ModelState.IsValid)
            {
                intergalacticDonation.CreateDate = DateTime.Now;

                _context.Add(intergalacticDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DonorId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["MissionId"] = intergalacticDonation.MissionId;
            ViewData["PackageId"] = new SelectList(_context.IntergalacticPackages, "Id", "Name", intergalacticDonation.PackageId);
            return View(intergalacticDonation);
        }

        private bool IntergalacticDonationExists(int id)
        {
          return (_context.IntergalacticDonation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
