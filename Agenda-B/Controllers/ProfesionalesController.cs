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
using Agenda_B.ViewModels;

namespace Agenda_B.Controllers
{
    public class ProfesionalesController : Controller
    {
        private readonly AgendaContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signinManager;

        public ProfesionalesController(AgendaContext context, UserManager<Persona> userManager, SignInManager<Persona> signinManager)
        {
            _context = context;
            this._userManager = userManager;
            this._signinManager = signinManager;
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Index()
        {
            var agendaContext = _context.Profesionales.Include(p => p.Prestacion);
            return View(await agendaContext.ToListAsync());
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL)]
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

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Create()
        {
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Create(RegistracionProfesional registracionProfesional)
        {
            if (ModelState.IsValid)
            {
                Profesional profesional = new Profesional();
                profesional.Email = registracionProfesional.Email;
                profesional.UserName = registracionProfesional.Email;
                profesional.Nombre = registracionProfesional.Nombre;
                profesional.Apellido = registracionProfesional.Apellido;
                profesional.DNI = registracionProfesional.DNI;
                profesional.Telefono = registracionProfesional.Telefono;
                profesional.Matricula = registracionProfesional.Matricula;
                profesional.HoraInicio = registracionProfesional.HoraInicio;
                profesional.HoraFin = registracionProfesional.HoraFin;
                profesional.PrestacionId = registracionProfesional.PrestacionId;

                var resultCreateUser = await _userManager.CreateAsync(profesional, registracionProfesional.Password);

                if (resultCreateUser.Succeeded)
                {
                    var resultAddRole = await _userManager.AddToRoleAsync(profesional,
                        Constantes.ROL_NOMBRE_PROFESIONAL);

                    if (resultAddRole.Succeeded)
                    {
                        return RedirectToAction("Create", "Direcciones", new { id = profesional.Id });
                    }
                }
                if (resultCreateUser.Errors.Count() > 0)
                {
                    string errorMessage = string.Empty;
                    foreach (var error in resultCreateUser.Errors)
                    {
                        errorMessage += $"{error.Description}\n";
                    }
                    ModelState.AddModelError("Password", errorMessage);
                }
            }
            ViewData["PrestacionId"] = new SelectList(_context.Prestaciones, "Id", "Nombre");
            return View(registracionProfesional);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Edit(int id, [Bind("Id,Matricula,PrestacionId,HoraInicio,HoraFin,Nombre,Apellido,DNI,Email,Telefono,Password")] Profesional profesional)
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
                    _context.SaveChanges();
                 
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

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
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
