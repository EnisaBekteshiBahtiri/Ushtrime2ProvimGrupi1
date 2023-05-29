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
    public class ShkrimetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShkrimetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shkrimets
        public async Task<IActionResult> Index(string emri)
        {
            if (string.IsNullOrEmpty(emri))
            {
                var appDbContext = _context.Shkrimets.Include(s => s.Autori).Include(s => s.Kategoria);
                return View(await appDbContext.ToListAsync());
            }
            
            var list = await _context.Shkrimets.Include(s => s.Autori).Include(s => s.Kategoria).Where(s => s.Kategoria.Emri == emri).ToListAsync();
            
            return View(list);
        }

        // GET: Shkrimets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shkrimet = await _context.Shkrimets
                .Include(s => s.Autori)
                .Include(s => s.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shkrimet == null)
            {
                return NotFound();
            }

            return View(shkrimet);
        }

        // GET: Shkrimets/Create
        public IActionResult Create()
        {
            ViewData["AutoriId"] = new SelectList(_context.Autoris, "Id", "Emri");
            ViewData["KategoriaId"] = new SelectList(_context.Kategoria, "Id", "Emri");
            return View();
        }

        // POST: Shkrimets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulli,AutoriId,KategoriaId")] Shkrimet shkrimet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shkrimet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoriId"] = new SelectList(_context.Autoris, "Id", "Emri", shkrimet.AutoriId);
            ViewData["KategoriaId"] = new SelectList(_context.Kategoria, "Id", "Emri", shkrimet.KategoriaId);
            return View(shkrimet);
        }

        // GET: Shkrimets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shkrimet = await _context.Shkrimets.FindAsync(id);
            if (shkrimet == null)
            {
                return NotFound();
            }
            ViewData["AutoriId"] = new SelectList(_context.Autoris, "Id", "Emri", shkrimet.AutoriId);
            ViewData["KategoriaId"] = new SelectList(_context.Kategoria, "Id", "Emri", shkrimet.KategoriaId);
            return View(shkrimet);
        }

        // POST: Shkrimets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulli,AutoriId,KategoriaId")] Shkrimet shkrimet)
        {
            if (id != shkrimet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shkrimet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShkrimetExists(shkrimet.Id))
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
            ViewData["AutoriId"] = new SelectList(_context.Autoris, "Id", "Emri", shkrimet.AutoriId);
            ViewData["KategoriaId"] = new SelectList(_context.Kategoria, "Id", "Emri", shkrimet.KategoriaId);
            return View(shkrimet);
        }

        // GET: Shkrimets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shkrimet = await _context.Shkrimets
                .Include(s => s.Autori)
                .Include(s => s.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shkrimet == null)
            {
                return NotFound();
            }

            return View(shkrimet);
        }

        // POST: Shkrimets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shkrimet = await _context.Shkrimets.FindAsync(id);
            _context.Shkrimets.Remove(shkrimet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShkrimetExists(int id)
        {
            return _context.Shkrimets.Any(e => e.Id == id);
        }
    }
}
