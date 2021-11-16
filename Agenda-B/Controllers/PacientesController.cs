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
    public class PacientesController : Controller
    {
        private readonly AgendaContext _context;

        public PacientesController(AgendaContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var listaPacientes = _context.Pacientes.ToList();

            return View(listaPacientes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(paciente => paciente.Direccion)
                .Include(paciente => paciente.Formularios)
                .Include(paciente => paciente.Turnos)
                .ThenInclude(turno => turno.Profesional)            
                .FirstOrDefaultAsync(m => m.Id == id);

            paciente.Turnos = paciente.Turnos
                    .Where(turno => !turno.Cancelado && turno.Fecha > DateTime.Now)
                    .OrderBy(turno => turno.Fecha).ToList();

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObraSocial,Id,Nombre,Apellido,DNI,Email,Telefono,Password")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Direcciones");
            }
            return View(paciente);
        }

        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Turnos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + ", " + Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult Edit(int id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Paciente pct = _context.Pacientes.Find(paciente.Id);                 
                    pct.Email = paciente.Email;
                    pct.Telefono = paciente.Telefono;
                    pct.ObraSocial = paciente.ObraSocial;

                    _context.Update(pct);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
