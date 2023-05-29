using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ushtrime2ProvimGrupi1.Entities;

namespace Ushtrime2ProvimGrupi1.Controllers
{
    public class KategoriumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategoriumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategoriums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategoria.ToListAsync());
        }

        // GET: Kategoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorium = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorium == null)
            {
                return NotFound();
            }

            return View(kategorium);
        }

        // GET: Kategoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri")] Kategorium kategorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorium);
        }

        // GET: Kategoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorium = await _context.Kategoria.FindAsync(id);
            if (kategorium == null)
            {
                return NotFound();
            }
            return View(kategorium);
        }

        // POST: Kategoriums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri")] Kategorium kategorium)
        {
            if (id != kategorium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriumExists(kategorium.Id))
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
            return View(kategorium);
        }

        // GET: Kategoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorium = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorium == null)
            {
                return NotFound();
            }

            return View(kategorium);
        }

        // POST: Kategoriums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorium = await _context.Kategoria.FindAsync(id);
            _context.Kategoria.Remove(kategorium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriumExists(int id)
        {
            return _context.Kategoria.Any(e => e.Id == id);
        }
    }
}
