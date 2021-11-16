using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda_B.Data;
using Agenda_B.Models;

namespace Agenda_B.Controllers
{
    public class PrestacionesController : Controller
    {
        private readonly AgendaContext _context;

        public PrestacionesController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Prestaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestaciones.ToListAsync());
        }

        // GET: Prestaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestaciones
                .Include(p => p.Profesionales)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        // GET: Prestaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prestaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Duracion,Precio")] Prestacion prestacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestacion);
        }

        // GET: Prestaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestaciones
                .Include(p => p.Profesionales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestacion == null)
            {
                return NotFound();
            }
            return View(prestacion);
        }

        // POST: Prestaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Duracion,Precio")] Prestacion prestacion)
        {
            if (id != prestacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestacionExists(prestacion.Id))
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
            return View(prestacion);
        }

        // GET: Prestaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        // POST: Prestaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestacion = await _context.Prestaciones.FindAsync(id);
            _context.Prestaciones.Remove(prestacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestacionExists(int id)
        {
            return _context.Prestaciones.Any(e => e.Id == id);
        }
    }
}
