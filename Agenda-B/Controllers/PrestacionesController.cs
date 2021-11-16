using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda_B.Data;
using Agenda_B.Models;
using Microsoft.AspNetCore.Authorization;
using Agenda_B.Helpers;

namespace Agenda_B.Controllers
{
    public class PrestacionesController : Controller
    {
        private readonly AgendaContext _context;

        public PrestacionesController(AgendaContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestaciones.ToListAsync());
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Create()
        {
            return View(new Prestacion());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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
