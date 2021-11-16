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
using Agenda_B.ViewModels;
using Microsoft.AspNetCore.Identity;
using Agenda_B.Helpers;

namespace Agenda_B.Controllers
{
    public class TurnosController : Controller
    {
        private readonly AgendaContext _context;
        private List<DateTime> turnosDisponibles = new List<DateTime>();
        private readonly UserManager<Persona> _userManager;

        public TurnosController(AgendaContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Index()
        {
            List<Turno> turnos;
                turnos = _context.Turnos
                    .Include(t => t.Paciente)
                    .Include(t => t.Profesional)
                    .Where(turno => turno.Fecha >= DateTime.Now && turno.Cancelado == false)
                    .OrderBy(t => t.Fecha).ToList();
            
            return View(turnos);
        }

        

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult SeleccionarPrestacion()
        {          

            ViewBag.ListadoPrestaciones = _context.Prestaciones.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult SeleccionarPrestacion(int? prestacionId)
        {
            if (prestacionId == null) {
                return View();
            }

            return RedirectToAction("SeleccionarProfesional", new { prestacionId = prestacionId });
        }

        public IActionResult SeleccionarProfesional(int? prestacionId)
        {            
           
            ViewBag.profesionales =  _context
                .Profesionales.Where(a => a.PrestacionId == prestacionId)
                .ToList();
            
            return View();
        }

        [HttpPost]
        public IActionResult SeleccionarProfesional(int? profesionalId, Turno turno)
        {
            if (profesionalId == null)
            {
                return View();
            }
            return RedirectToAction("SeleccionarHorarios", new { profesionalId = profesionalId });
        }
        
        public async Task<IActionResult> SeleccionarHorarios(int? profesionalId)
        {
            Profesional profesional = _context.Profesionales.Include(p => p.Prestacion)
                .Include(p => p.Turnos)
                .FirstOrDefault( p => p.Id == profesionalId);

            Paciente paciente = (Paciente) await _userManager.GetUserAsync(this.User);

            List<Turno> turnosDisponibles = obtenerTurnosDisponibles(profesional, paciente.Turnos);

            TurnoDisponibleViewModel viewModel = new TurnoDisponibleViewModel
            {
                FechasDisponibles = turnosDisponibles.Select(t => t.Fecha).ToList(),
                Profesional = profesional,
                Prestacion = profesional.Prestacion,
                PacienteId = paciente.Id
            };

            return View(viewModel);
        }

        private List<Turno> obtenerTurnosDisponiblesProfesional(Profesional profesional) {
            List<Turno> turnosDisponibles = crearTurnosDisponibles(profesional);
            return removerTurnosReservados(turnosDisponibles, profesional.Turnos);
        }
        private List<Turno> obtenerTurnosDisponibles(Profesional profesional, List<Turno> turnos)
        {
            List<Turno> turnosDisponiblesProfesional = obtenerTurnosDisponiblesProfesional(profesional);
            return  removerTurnosReservados(turnosDisponiblesProfesional, turnos);
        }

        private List<Turno> crearTurnosDisponibles(Profesional profesional)
        {
            List<Turno> turnos = new List<Turno>();
            for (int i = 0; i < Constantes.DIAS_AUTORIZADOS_TURNO; i++)
            {
                List<DateTime> turnosDisponiblesPorDia = profesional.HorariosDisponiblesPorDia;
                foreach (DateTime horarioDisponible in turnosDisponiblesPorDia)
                {
                    Turno turno = new Turno();
                    DateTime now = DateTime.Now;
                    turno.Fecha = new DateTime(now.Year, now.Month, now.Day, horarioDisponible.Hour,
                        horarioDisponible.Minute, horarioDisponible.Second).AddDays(i);
                    turno.Profesional = profesional;
                    turno.ProfesionalId = profesional.Id;
                    turnos.Add(turno);
                }
            }
            return turnos.Where(turno => turno.Fecha >= DateTime.Now).ToList();
        }

        private List<Turno> removerTurnosReservados(List<Turno> turnos, List<Turno> turnosReservados)
        {
            List<Turno> disponibles = new List<Turno>();
            disponibles.AddRange(turnos);

            if (turnosReservados != null && turnosReservados.Count() > 0) { 
                foreach (Turno turno in turnos)
                {
                    foreach (Turno reservado in turnosReservados)
                    {
                        if (reservado.Fecha == turno.Fecha) {
                            disponibles.Remove(turno);
                        }
                    }
                }
            }
            return disponibles;
        }

        [HttpPost]
        public IActionResult SeleccionarHorarios(TurnoDisponibleViewModel model)
        {
            Turno nuevoTurno = new Turno();
            nuevoTurno.Fecha = model.FechaTurno;
            nuevoTurno.PacienteId = model.PacienteId;
            nuevoTurno.ProfesionalId = model.ProfesionalId;
            _context.Turnos.Add(nuevoTurno);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", turno.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", turno.ProfesionalId);
            return View(turno);
        }


        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL)]
        public async Task<IActionResult> Confirmar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            try
            {               
                turno.Confirmado = true;                                         
                _context.Update(turno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(turno.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Turnos");           
        }


        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public async Task<IActionResult> Cancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN + "," + Constantes.ROL_NOMBRE_PROFESIONAL + "," + Constantes.ROL_NOMBRE_PACIENTE)]
        public IActionResult Cancelar(int id, [Bind("Id,DescripcionCancelacion")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Turno turnoCancelado = _context.Turnos.Find(id);
                    turnoCancelado.DescripcionCancelacion = turno.DescripcionCancelacion;
                    turnoCancelado.Confirmado = false;
                    turnoCancelado.Cancelado = true;
                    _context.Update(turnoCancelado);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Turnos");
            }
            return View(turno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Confirmado,Activo,FechaAlta,DescripcionCancelacion,PacienteId,ProfesionalId")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", turno.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "NombreCompleto", turno.ProfesionalId);
            return View(turno);
        }

        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Cancelados()
        {
            List<Turno> turnosCancelados;
            turnosCancelados = _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .Where(turno => turno.Fecha >= DateTime.Now && turno.Cancelado == true)
                .OrderBy(t => t.Fecha).ToList();

            return View(turnosCancelados);
        }



        [Authorize(Roles = Constantes.ROL_NOMBRE_ADMIN)]
        public IActionResult Activar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = _context.Turnos.Find(id);
            if (turno == null)
            {
                return NotFound();
            }

            try
            {
                if(!_context.Turnos.Where(t => t.PacienteId == turno.PacienteId).Any(t => t.Activo))
                {
                    turno.Activo = true;
                    _context.Update(turno);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(turno.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Turnos");
        }
        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}

