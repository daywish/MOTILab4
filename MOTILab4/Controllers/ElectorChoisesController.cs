using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOTILab4.Data;
using MOTILab4.Models;
using MOTILab4.VIewModels;

namespace MOTILab4.Controllers
{
    public class ElectorChoisesController : Controller
    {
        private readonly MOTILab4Context _context;

        public ElectorChoisesController(MOTILab4Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateAllChoises()
        {
            if (!_context.Elector.Any() || !_context.VotingObject.Any())
            {
                return NotFound();
            }
            ChoiseCreateViewModel model = new ChoiseCreateViewModel();
            model.Electors = await _context.Elector.ToListAsync();
            model.VotingObjects = await _context.VotingObject.ToListAsync();
            ViewData["ElectorId"] = new SelectList(_context.Elector, "ElectorId", "ElectorName");
            ViewData["VotingObjectId"] = new SelectList(_context.VotingObject, "VotingObjectId", "VotingObjectName");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllChoises(string selectionString)
        {
            string electorId = selectionString.Substring(0, selectionString.IndexOf("/"));
            int id = int.Parse(electorId);
            Elector elector = await _context.Elector.Where(i => i.ElectorId == id).FirstOrDefaultAsync();
            int[] order = new int[_context.VotingObject.Count()];
            int size = electorId.Length + 1;
            selectionString = selectionString.Substring(size, selectionString.Length - size);
            for(int i = 0; i<order.Length; i++)
            {
                string idObj = selectionString.Substring(0, selectionString.IndexOf("|"));
                order[i] = int.Parse(idObj);
                size = idObj.Length + 1;
                selectionString = selectionString.Substring(size, selectionString.Length - size);
            }
            for(int i = 0; i<order.Length; i++)
            {
                int idObj = order[i];
                VotingObject vObject = await _context.VotingObject.FirstAsync(i => i.VotingObjectId== idObj);
                ElectorChoise newChoise = new()
                {
                    ElectorId = elector.ElectorId,
                    Elector = elector,
                    VotingObjectId = vObject.VotingObjectId,
                    VotingObject = vObject,
                    Order = i + 1
                };
                _context.ElectorChoise.Add(newChoise);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ElectorChoises
        public async Task<IActionResult> Index()
        {
            var mOTILab4Context = _context.ElectorChoise.Include(e => e.Elector).Include(v=>v.VotingObject);
            return View(await mOTILab4Context.ToListAsync());
        }

        // GET: ElectorChoises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electorChoise = await _context.ElectorChoise
                .Include(e => e.Elector)
                .Include(v=>v.VotingObject)
                .FirstOrDefaultAsync(m => m.ElectorChoiseId == id);
            if (electorChoise == null)
            {
                return NotFound();
            }

            return View(electorChoise);
        }

        
        public IActionResult Create()
        {
            ViewData["ElectorId"] = new SelectList(_context.Elector, "ElectorId", "ElectorName");
            ViewData["VotingObjectId"] = new SelectList(_context.VotingObject, "VotingObjectId", "VotingObjectName");
            return View();
        }

        // POST: ElectorChoises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectorChoiseId,ElectorId,VotingObjectId,Order")] ElectorChoise electorChoise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electorChoise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElectorId"] = new SelectList(_context.Elector, "ElectorId", "ElectorId", electorChoise.ElectorId);
            return View(electorChoise);
        }

        // GET: ElectorChoises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electorChoise = await _context.ElectorChoise.FindAsync(id);
            if (electorChoise == null)
            {
                return NotFound();
            }
            ViewData["ElectorId"] = new SelectList(_context.Elector, "ElectorId", "ElectorId", electorChoise.ElectorId);
            return View(electorChoise);
        }

        // POST: ElectorChoises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectorChoiseId,ElectorId,VotingObjectId,Order")] ElectorChoise electorChoise)
        {
            if (id != electorChoise.ElectorChoiseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electorChoise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectorChoiseExists(electorChoise.ElectorChoiseId))
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
            ViewData["ElectorId"] = new SelectList(_context.Elector, "ElectorId", "ElectorId", electorChoise.ElectorId);
            return View(electorChoise);
        }

        // GET: ElectorChoises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electorChoise = await _context.ElectorChoise
                .Include(e => e.Elector)
                .FirstOrDefaultAsync(m => m.ElectorChoiseId == id);
            if (electorChoise == null)
            {
                return NotFound();
            }

            return View(electorChoise);
        }

        // POST: ElectorChoises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electorChoise = await _context.ElectorChoise.FindAsync(id);
            _context.ElectorChoise.Remove(electorChoise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAll()
        {
            if (!_context.ElectorChoise.Any())
            {
                return NotFound();
            }
            List<ElectorChoise> electorChoises = await _context.ElectorChoise.ToListAsync();
            foreach(var choise in electorChoises)
            {
                _context.ElectorChoise.Remove(choise);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ElectorChoiseExists(int id)
        {
            return _context.ElectorChoise.Any(e => e.ElectorChoiseId == id);
        }
    }
}
