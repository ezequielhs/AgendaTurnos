using Agenda_B.Data;
using Agenda_B.Models;
using Agenda_B.ViewModels;
using Agenda_B.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Controllers
{
    public class CuentaController : Controller
    {
        private readonly AgendaContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signinManager;
        private readonly RoleManager<IdentityRole<int>> _rolManager;

        public CuentaController(
            AgendaContext context,
            UserManager<Persona> userManager,
            SignInManager<Persona> signinManager,
            RoleManager<IdentityRole<int>> rolManager
            )
        {
            this._context = context;
            this._userManager = userManager;
            this._signinManager = signinManager;
            this._rolManager = rolManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrarse()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Registrarse(RegistracionViewModel registracionModel)
        {
            if (ModelState.IsValid)
            {
                Paciente nuevoPaciente = new Paciente();
                nuevoPaciente.Email = registracionModel.Email;
                nuevoPaciente.UserName = registracionModel.Email;
                nuevoPaciente.Nombre = registracionModel.Nombre;
                nuevoPaciente.Apellido = registracionModel.Apellido;
                nuevoPaciente.DNI = registracionModel.DNI;
                nuevoPaciente.Telefono = registracionModel.Telefono;
                nuevoPaciente.ObraSocial = registracionModel.ObraSocial;

                var resultCreateUser = await _userManager.CreateAsync(nuevoPaciente, registracionModel.Password);
                
                if (resultCreateUser.Succeeded)
                {
                    var resultAddRole = await _userManager.AddToRoleAsync(nuevoPaciente,
                        Constantes.ROL_NOMBRE_PACIENTE);

                    if (resultAddRole.Succeeded)
                    {
                        await _signinManager.SignInAsync(nuevoPaciente, false);
                        return RedirectToAction("Create", "Direcciones", new { id = nuevoPaciente.Id });
                    }
                }
                if(resultCreateUser.Errors.Count() > 0)
                {
                    string errorMessage = string.Empty;
                    foreach (var error in resultCreateUser.Errors)
                    {
                        errorMessage += $"{error.Description}\n";
                    }
                    ModelState.AddModelError("Password", errorMessage);
                }
            }

            return View(registracionModel);
        }

        public ActionResult IniciarSesion(string returnurl)
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Login viewModel)
        {
            if (ModelState.IsValid)
            {
                var resultadoSignIn = await _signinManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false);

                if (resultadoSignIn.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> CerrarSesion()
        {
            await _signinManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

    }
}
