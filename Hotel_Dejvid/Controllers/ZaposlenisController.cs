using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Dejvid.Data;
using Hotel_Dejvid.Models;

namespace Hotel_Dejvid.Controllers
{
    public class ZaposlenisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZaposlenisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zaposlenis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zaposleni.ToListAsync());
        }

        // GET: Zaposlenis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleni = await _context.Zaposleni
                .FirstOrDefaultAsync(m => m.ZaposleniId == id);
            if (zaposleni == null)
            {
                return NotFound();
            }

            return View(zaposleni);
        }

        // GET: Zaposlenis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zaposlenis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZaposleniId,Ime,Prezime,Pozicija,BrojTelefona")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposleni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaposleni);
        }

        // GET: Zaposlenis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleni = await _context.Zaposleni.FindAsync(id);
            if (zaposleni == null)
            {
                return NotFound();
            }
            return View(zaposleni);
        }

        // POST: Zaposlenis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZaposleniId,Ime,Prezime,Pozicija,BrojTelefona")] Zaposleni zaposleni)
        {
            if (id != zaposleni.ZaposleniId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposleni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposleniExists(zaposleni.ZaposleniId))
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
            return View(zaposleni);
        }

        // GET: Zaposlenis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleni = await _context.Zaposleni
                .FirstOrDefaultAsync(m => m.ZaposleniId == id);
            if (zaposleni == null)
            {
                return NotFound();
            }

            return View(zaposleni);
        }

        // POST: Zaposlenis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposleni = await _context.Zaposleni.FindAsync(id);
            if (zaposleni != null)
            {
                _context.Zaposleni.Remove(zaposleni);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposleniExists(int id)
        {
            return _context.Zaposleni.Any(e => e.ZaposleniId == id);
        }
    }
}
