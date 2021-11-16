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
    public class ProfesionalesController : Controller
    {
        private readonly AgendaContext _context;

        public ProfesionalesController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Profesional
        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.Profesionales.Include(p => p.Prestacion);
            return View(await agendaContext.ToListAsync());
        }

        // GET: Profesional/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .Include(d => d.Direccion)
                .Include(p => p.Prestacion)
                .Include(t => t.Turnos)
                .ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesional/Create
        public IActionResult Create()
        {
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre");
            return View();
        }

        // POST: Profesional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,PrestacionId,HoraInicio,HoraFin,Id,Nombre,Apellido,DNI,Email,Telefono,FechaAlta,Password")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                profesional.FechaAlta = DateTime.Now;
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre", profesional.PrestacionId);
            return View(profesional);
        }

        // GET: Profesional/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre", profesional.PrestacionId);
            return View(profesional);
        }

        // POST: Profesional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,PrestacionId,HoraInicio,HoraFin,Nombre,Apellido,DNI,Email,Telefono,Password")] Profesional profesional)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Profesional prf = _context.Profesionales.Find(profesional.Id);
                    prf.Matricula = profesional.Matricula;
                    prf.Prestacion = _context.Prestaciones.Find(profesional.PrestacionId);
                    prf.HoraInicio = profesional.HoraInicio;
                    prf.HoraFin = profesional.HoraFin;
                    prf.Nombre = profesional.Nombre;
                    prf.Apellido = profesional.Apellido;
                    prf.DNI = profesional.DNI;
                    prf.Email = profesional.Email;
                    prf.Telefono = profesional.Telefono;

                    _context.Update(prf);
                    await _context.SaveChangesAsync();
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionalExists(profesional.Id))
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
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre", profesional.PrestacionId);
            return View(profesional);
        }

        // GET: Profesional/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .Include(p => p.Prestacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            _context.Profesionales.Remove(profesional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionalExists(int id)
        {
            return _context.Profesionales.Any(e => e.Id == id);
        }
    }
}
