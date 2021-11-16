using Agenda_B.Data;
using Agenda_B.Models;
using Agenda_B.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Agenda_B.Controllers
{
    public class HomeController : Controller
    {
        private readonly AgendaContext _context;
        private readonly UserManager<Persona> _userManager;

        public HomeController(AgendaContext context, UserManager<Persona> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole("Paciente"))
            {
                int idPaciente = int.Parse(_userManager.GetUserId(this.User));
                return RedirectToAction("Details", "Pacientes", new { id = idPaciente});
            }
            return View();
        }

        public IActionResult Nosotros()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
