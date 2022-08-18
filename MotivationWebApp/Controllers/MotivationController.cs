using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotivationWebApp.Data;
using MotivationWebApp.Models;

namespace MotivationWebApp.Controllers
{
    public class MotivationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotivationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Motivations
        public async Task<IActionResult> Index()
        {
              return _context.Motivation != null ? 
                          View(await _context.Motivation.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Motivation'  is null.");
        }

        // GET: Motivations/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Motivation != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Motivation'  is null.");
        }

        // POST: Motivations/ShowResultsForm
        public async Task<IActionResult> ShowResultsForm(String SearchPhrase)
        {
            return _context.Motivation != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Motivation'  is null.");
        }

        // GET: Motivations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motivation == null)
            {
                return NotFound();
            }

            var motivation = await _context.Motivation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivation == null)
            {
                return NotFound();
            }

            return View(motivation);
        }

        // GET: Motivations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motivations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MotivationType,MotivationContent")] Motivation motivation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motivation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motivation);
        }

        // GET: Motivations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motivation == null)
            {
                return NotFound();
            }

            var motivation = await _context.Motivation.FindAsync(id);
            if (motivation == null)
            {
                return NotFound();
            }
            return View(motivation);
        }

        // POST: Motivations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MotivationType,MotivationContent")] Motivation motivation)
        {
            if (id != motivation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motivation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotivationExists(motivation.Id))
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
            return View(motivation);
        }

        // GET: Motivations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motivation == null)
            {
                return NotFound();
            }

            var motivation = await _context.Motivation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivation == null)
            {
                return NotFound();
            }

            return View(motivation);
        }

        // POST: Motivations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Motivation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Motivation'  is null.");
            }
            var motivation = await _context.Motivation.FindAsync(id);
            if (motivation != null)
            {
                _context.Motivation.Remove(motivation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotivationExists(int id)
        {
          return (_context.Motivation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
