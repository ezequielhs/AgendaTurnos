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
using Microsoft.AspNetCore.Identity;

namespace Agenda_B.Controllers
{
    public class FormulariosController : Controller
    {
        private readonly AgendaContext _context;
        private readonly UserManager<Persona> _userManager;

        public FormulariosController(AgendaContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.Formularios.Include(f => f.Paciente);
            return View(await agendaContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
                .Include(f => f.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }
        
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Set<Persona>(), "Id", "NombreCompleto");
            if (this.User.Identity.IsAuthenticated)
            {
                Formulario formulario = new Formulario();
                int pacienteId = int.Parse(_userManager.GetUserId(this.User));
                Paciente paciente = _context.Pacientes.Where(paciente => paciente.Id == pacienteId).FirstOrDefault();
                formulario.Apellido = paciente.Nombre;
                formulario.Nombre = paciente.Apellido;
                formulario.Email = paciente.Email;
                formulario.PacienteId = paciente.Id;
                return View(formulario);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formulario);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(formulario);
        }

        [Authorize(Roles= Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Set<Persona>(), "Id", "NombreCompleto", formulario.PacienteId);
            return View(formulario);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Email,Nombre,Apellido,Leido,Titulo,Mensaje,UsuarioId")] Formulario formulario)
        {
            if (id != formulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formulario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioExists(formulario.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Set<Persona>(), "Id", "NombreCompleto", formulario.PacienteId);
            return View(formulario);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public  IActionResult Leer(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Formulario formulario = _context.Formularios.Find(id);
                formulario.Leido = true;
                _context.Update(formulario);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool FormularioExists(int id)
        {
            return _context.Formularios.Any(e => e.Id == id);
        }
    }
}
