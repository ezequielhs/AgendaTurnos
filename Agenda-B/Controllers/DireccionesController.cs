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

namespace Agenda_B
{

    public class DireccionesController : Controller
    {
        private readonly AgendaContext _context;

        public DireccionesController(AgendaContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Index()
        { 
            var listaDirecciones = _context.Direcciones.Include(d => d.Persona);

            return View(await listaDirecciones.ToListAsync());
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }
            return View(direccion);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult Create(int? id)
        {
            
            Persona paciente = _context
                .Personas.Where(a => a.Id == id).FirstOrDefault();

            if(paciente != null)
            {
                ViewBag.pacienteId = paciente.Id;
            }            
           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult Create([Bind("Id,Calle,Numero,CodigoPostal,Localidad,Provincia")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                _context.Direcciones.Add(direccion);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(direccion);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Set<Persona>(), "Id", "NombreCompleto", direccion.Id);
            return View(direccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult Edit(int id, [Bind("Id,Calle,Numero,CodigoPostal,Localidad,Provincia")] Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(direccion);
        }
        private bool DireccionExists(int id)
        {
            return _context.Direcciones.Any(e => e.Id == id);
        }
    }
}
