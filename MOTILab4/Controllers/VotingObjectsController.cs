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
    public class VotingObjectsController : Controller
    {
        private readonly MOTILab4Context _context;

        public VotingObjectsController(MOTILab4Context context)
        {
            _context = context;
        }

        // GET: VotingObjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.VotingObject.ToListAsync());
        }

        // GET: VotingObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingObject = await _context.VotingObject
                .FirstOrDefaultAsync(m => m.VotingObjectId == id);
            if (votingObject == null)
            {
                return NotFound();
            }

            return View(votingObject);
        }

        // GET: VotingObjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VotingObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VotingObjectId,VotingObjectName")] VotingObject votingObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(votingObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(votingObject);
        }

        // GET: VotingObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingObject = await _context.VotingObject.FindAsync(id);
            if (votingObject == null)
            {
                return NotFound();
            }
            return View(votingObject);
        }

        // POST: VotingObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VotingObjectId,VotingObjectName")] VotingObject votingObject)
        {
            if (id != votingObject.VotingObjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(votingObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotingObjectExists(votingObject.VotingObjectId))
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
            return View(votingObject);
        }

        // GET: VotingObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingObject = await _context.VotingObject
                .FirstOrDefaultAsync(m => m.VotingObjectId == id);
            if (votingObject == null)
            {
                return NotFound();
            }

            return View(votingObject);
        }

        // POST: VotingObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votingObject = await _context.VotingObject.FindAsync(id);
            _context.VotingObject.Remove(votingObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotingObjectExists(int id)
        {
            return _context.VotingObject.Any(e => e.VotingObjectId == id);
        }
    }
}
