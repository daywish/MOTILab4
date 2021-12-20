using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOTILab4.Data;
using MOTILab4.Models;

namespace MOTILab4.Controllers
{
    public class ElectorsController : Controller
    {
        private readonly MOTILab4Context _context;

        public ElectorsController(MOTILab4Context context)
        {
            _context = context;
        }

        // GET: Electors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elector.ToListAsync());
        }

        // GET: Electors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elector = await _context.Elector
                .FirstOrDefaultAsync(m => m.ElectorId == id);
            if (elector == null)
            {
                return NotFound();
            }

            return View(elector);
        }

        // GET: Electors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Electors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectorId,ElectorName,ElectorRatio")] Elector elector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(elector);
        }

        // GET: Electors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elector = await _context.Elector.FindAsync(id);
            if (elector == null)
            {
                return NotFound();
            }
            return View(elector);
        }

        // POST: Electors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectorId,ElectorName,ElectorRatio")] Elector elector)
        {
            if (id != elector.ElectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectorExists(elector.ElectorId))
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
            return View(elector);
        }

        // GET: Electors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elector = await _context.Elector
                .FirstOrDefaultAsync(m => m.ElectorId == id);
            if (elector == null)
            {
                return NotFound();
            }

            return View(elector);
        }

        // POST: Electors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elector = await _context.Elector.FindAsync(id);
            _context.Elector.Remove(elector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectorExists(int id)
        {
            return _context.Elector.Any(e => e.ElectorId == id);
        }
    }
}
