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
    public class GostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gost.ToListAsync());
        }

        // GET: Gosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gost = await _context.Gost
                .FirstOrDefaultAsync(m => m.GostId == id);
            if (gost == null)
            {
                return NotFound();
            }

            return View(gost);
        }

        // GET: Gosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GostId,Ime,Prezime,Email,BrojTelefona")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gost);
        }

        // GET: Gosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gost = await _context.Gost.FindAsync(id);
            if (gost == null)
            {
                return NotFound();
            }
            return View(gost);
        }

        // POST: Gosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GostId,Ime,Prezime,Email,BrojTelefona")] Gost gost)
        {
            if (id != gost.GostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GostExists(gost.GostId))
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
            return View(gost);
        }

        // GET: Gosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gost = await _context.Gost
                .FirstOrDefaultAsync(m => m.GostId == id);
            if (gost == null)
            {
                return NotFound();
            }

            return View(gost);
        }

        // POST: Gosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gost = await _context.Gost.FindAsync(id);
            if (gost != null)
            {
                _context.Gost.Remove(gost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GostExists(int id)
        {
            return _context.Gost.Any(e => e.GostId == id);
        }
    }
}
