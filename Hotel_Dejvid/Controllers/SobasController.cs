using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Dejvid.Data;
using Hotel_Dejvid.Models;

namespace Hotel_Dejvid.Controllers
{
    public class SobasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SobasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sobas
        public async Task<IActionResult> Index(bool? isAvailable, decimal? maxPricePerNight, string sortOrder)
        {
            // Postavi početni upit za sobe
            var query = _context.Soba.AsQueryable();

            // Filtriraj sobe prema dostupnosti
            if (isAvailable.HasValue)
            {
                query = query.Where(r => r.Dostupna == isAvailable.Value);
            }

            // Filtriraj sobe prema maksimalnoj ceni po noći
            if (maxPricePerNight.HasValue)
            {
                query = query.Where(r => r.CenaPoNoci <= maxPricePerNight.Value);
            }

            // Sortiraj sobe
            switch (sortOrder)
            {
                case "price_asc":
                    query = query.OrderBy(r => r.CenaPoNoci);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(r => r.CenaPoNoci);
                    break;
                case "availability":
                    query = query.OrderBy(r => r.Dostupna);
                    break;
                default:
                    query = query.OrderBy(r => r.SobaId); // Podrazumevani redosled
                    break;
            }

            // Dobavi podatke iz baze
            var rooms = await query.ToListAsync();
            return View(rooms);
        }

        // GET: Sobas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soba = await _context.Soba
                .FirstOrDefaultAsync(m => m.SobaId == id);
            if (soba == null)
            {
                return NotFound();
            }

            return View(soba);
        }

        // GET: Sobas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sobas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SobaId,BrojSobe,TipSobe,CenaPoNoci,Dostupna")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soba);
        }

        // GET: Sobas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soba = await _context.Soba.FindAsync(id);
            if (soba == null)
            {
                return NotFound();
            }
            return View(soba);
        }

        // POST: Sobas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SobaId,BrojSobe,TipSobe,CenaPoNoci,Dostupna")] Soba soba)
        {
            if (id != soba.SobaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SobaExists(soba.SobaId))
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
            return View(soba);
        }

        // GET: Sobas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soba = await _context.Soba
                .FirstOrDefaultAsync(m => m.SobaId == id);
            if (soba == null)
            {
                return NotFound();
            }

            return View(soba);
        }

        // POST: Sobas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soba = await _context.Soba.FindAsync(id);
            if (soba != null)
            {
                _context.Soba.Remove(soba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SobaExists(int id)
        {
            return _context.Soba.Any(e => e.SobaId == id);
        }
    }
}
