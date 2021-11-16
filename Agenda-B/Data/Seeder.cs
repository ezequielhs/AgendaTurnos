using Agenda_B.Helpers;
using Agenda_B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Data
{
    public class Seeder
    {
        private readonly AgendaContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<IdentityRole<int>> _rolManager;

        public Seeder(AgendaContext context, UserManager<Persona> userManager,RoleManager<IdentityRole<int>> rolManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._rolManager = rolManager;
        }

        public void Seed() {
            CrearRoles().Wait();
            CrearAdmin().Wait();
            GenerarPrestaciones();
            GenerarPacientes().Wait();
            GenerarProfesionales().Wait();
            GenerarDirecciones();
            GenerarFormularios();
            GenerarTurnos();
        }

        private async Task CrearAdmin()
        {
            Persona admin = new Persona()
            {
                Nombre = "Martín",
                Apellido = "Zbinden",
                Email = Constantes.DEFAULT_ADMIN_USERNAME,
                UserName = Constantes.DEFAULT_ADMIN_USERNAME
            };
            if (_context.Personas.FirstOrDefault(persona => persona.UserName == admin.UserName) == null)
            {
                var resultCreateUser = await _userManager.CreateAsync(admin, Constantes.DEFAULT_ADMIN_PASSWORD);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Constantes.ROL_NOMBRE_ADMIN);
                }
            }
        }

        private async Task CrearRol(string rolName)
        {
            if (!await _rolManager.RoleExistsAsync(rolName))
            {
                await _rolManager.CreateAsync(new IdentityRole<int>(rolName));
            }
        }

        public async Task CrearRoles()
        {
            List<string> roles = new List<string>() { Constantes.ROL_NOMBRE_ADMIN,
                Constantes.ROL_NOMBRE_PACIENTE,
                Constantes.ROL_NOMBRE_PROFESIONAL };

            foreach (string rol in roles)
            {
                await CrearRol(rol);
            }
        }

        private void GenerarPrestaciones()
        {
            if (_context.Prestaciones.Count() == 0)
            {
                List<Prestacion> prestaciones = new List<Prestacion>();
                prestaciones.Add(new Prestacion()
                {
                    Nombre = "Médico clínico",
                    Descripcion = "Clínica general",
                    Duracion = new TimeSpan(0, 30, 0),
                    Precio = 2000
                });

                prestaciones.Add(new Prestacion()
                {
                    Nombre = "Médico pediatra",
                    Descripcion = "Clínica especializada",
                    Duracion = new TimeSpan(1, 0, 0),
                    Precio = 8000
                });

                prestaciones.Add(new Prestacion()
                {
                    Nombre = "Médico kinesiólogo",
                    Descripcion = "Clínica especializada",
                    Duracion = new TimeSpan(3, 0, 0),
                    Precio = 4000
                });

                prestaciones.Add(new Prestacion()
                {
                    Nombre = "Médico ortodoncista",
                    Descripcion = "Clínica especializada",
                    Duracion = new TimeSpan(1, 30, 0),
                    Precio = 3000
                });
                _context.Prestaciones.AddRange(prestaciones);
                _context.SaveChanges();
            }
        }

        private async Task GenerarPacientes()
        {
            if (_context.Pacientes.Count() == 0)
            {
                List<Paciente> pacientes = new List<Paciente>();
                List<Direccion> direcciones = _context.Direcciones.ToList();

                pacientes.Add(new Paciente()
                {
                    ObraSocial = ObraSocial.LUIS_PASTEUR,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    DNI = "11742369",
                    Email = "juanperez@outlook.com",
                    UserName = "juanperez@outlook.com",
                    Telefono = "1142637459",
                    //Password = "Klprf_56",
                });
                pacientes.Add(new Paciente()
                {
                    ObraSocial = ObraSocial.OSECAC,
                    Nombre = "Leonardo",
                    Apellido = "Fernandez",
                    DNI = "40237856",
                    Email = "leofernandez@hotmail.com",
                    UserName = "leofernandez@hotmail.com",
                    Telefono = "1125486366",
                    //Password = "25637.!rf"
                });
                pacientes.Add(new Paciente()
                {
                    ObraSocial = ObraSocial.MEDICUS,
                    Nombre = "Matías",
                    Apellido = "Lopez",
                    DNI = "36520120",
                    Email = "matias-lopez15@hotmail.com",
                    UserName = "matias-lopez15@hotmail.com",
                    Telefono = "46320754",
                    //Password = "klñmPA.120"
                });
                pacientes.Add(new Paciente()
                {
                    ObraSocial = ObraSocial.PAMI,
                    Nombre = "Aldo",
                    Apellido = "Gimenez",
                    DNI = "11032410",
                    Email = "gimenez_aldo@gmail.com",
                    UserName = "gimenez_aldo@gmail.com",
                    Telefono = "45638974",
                    //Password = "wsdft_n450"
                });

                foreach (Paciente paciente in pacientes)
                {
                    var resultCreateUser = await _userManager.CreateAsync(paciente, "Password.1234");
                    if (resultCreateUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(paciente, Constantes.ROL_NOMBRE_PACIENTE);
                    }
                }
            }
        }

        private async Task GenerarProfesionales()
        {
            if (_context.Profesionales.Count() == 0)
            {
                List<Profesional> profesionales = new List<Profesional>();
                List<Prestacion> prestaciones = _context.Prestaciones.ToList();

                profesionales.Add(new Profesional()
                {
                    Matricula = "35887/1",
                    Nombre = "Fernando",
                    Apellido = "Goycochea",
                    DNI = "38301808",
                    Email = "goycochea_fernando@gmail.com",
                    UserName = "goycochea_fernando@gmail.com",
                    PrestacionId = prestaciones[0].Id,
                    HoraInicio = new DateTime().AddHours(5),
                    HoraFin = new DateTime().AddHours(8),
                    Telefono = "1164017782",
                });

                profesionales.Add(new Profesional()
                {
                    Matricula = "20045/30",
                    Nombre = "Lucia",
                    Apellido = "Martinez",
                    DNI = "35341838",
                    Email = "lucia.martinez@outlook.com",
                    UserName = "lucia.martinez@outlook.com",
                    PrestacionId = prestaciones[1].Id,
                    HoraInicio = new DateTime().AddHours(1),
                    HoraFin = new DateTime().AddHours(7),
                    Telefono = "1125853014",
                });

                profesionales.Add(new Profesional()
                {
                    Matricula = "7452/10",
                    Nombre = "Gladis",
                    Apellido = "Silvestre",
                    DNI = "25342945",
                    Email = "gladis_silvestre1150@yahoo.com.ar",
                    UserName = "gladis_silvestre1150@yahoo.com.ar",
                    PrestacionId = prestaciones[2].Id,
                    HoraInicio = new DateTime().AddHours(1),
                    HoraFin = new DateTime().AddHours(7),
                    Telefono = "41028563",
                });

                profesionales.Add(new Profesional()
                {
                    Matricula = "11256/8",
                    Nombre = "Romina",
                    Apellido = "Fiore",
                    DNI = "15856310",
                    Email = "fiore-romina@outlook.com",
                    UserName = "fiore-romina@outlook.com",
                    PrestacionId = prestaciones[3].Id,
                    HoraInicio = new DateTime().AddHours(1),
                    HoraFin = new DateTime().AddHours(7),
                    Telefono = "49630217",
                });

                foreach (Profesional profesional in profesionales)
                {
                    var resultCreateUser = await _userManager.CreateAsync(profesional, "Password.1234");
                    if (resultCreateUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(profesional, Constantes.ROL_NOMBRE_PROFESIONAL);
                    }
                }
            }
        }

        private void GenerarDirecciones()
        {
            if (_context.Direcciones.Count() == 0)
            {
                List<Direccion> direcciones = new List<Direccion>();
                List<Persona> personas = _context.Personas.ToList();

                direcciones.Add(new Direccion()
                {
                    Id = personas[0].Id,
                    Calle = "Bolivar",
                    Numero = "1042",
                    CodigoPostal = "1645",
                    Localidad = "Avellaneda",
                    Provincia = Provincia.BUENOS_AIRES,
                });

                direcciones.Add(new Direccion
                {
                    Id = personas[1].Id,
                    Calle = "Esteban Echeverria",
                    Numero = "4520",
                    CodigoPostal = "1863",
                    Localidad = "Gerli",
                    Provincia = Provincia.BUENOS_AIRES,
                });

                direcciones.Add(new Direccion
                {
                    Id = personas[2].Id,
                    Calle = "México",
                    Numero = "320",
                    CodigoPostal = "1160",
                    Localidad = "San Telmo",
                    Provincia = Provincia.CABA,
                });

                direcciones.Add(new Direccion
                {
                    Id = personas[3].Id,
                    Calle = "San Juan",
                    Numero = "2085",
                    CodigoPostal = "1975",
                    Localidad = "La Matanza",
                    Provincia = Provincia.BUENOS_AIRES,
                });
                _context.Direcciones.AddRange(direcciones);
                _context.SaveChanges();
            }
        }

        private void GenerarTurnos()
        {
            if (_context.Turnos.Count() == 0)
            {
                List<Turno> turnos = new List<Turno>();
                List<Paciente> pacientes = _context.Pacientes.ToList();
                List<Profesional> profesionales = _context.Profesionales.Include(p => p.Prestacion).ToList();
                DateTime now = DateTime.Now;

                turnos.Add(new Turno
                {
                    Fecha = new DateTime(now.Year, now.Month, now.Day, profesionales[0].HoraInicio.Hour, profesionales[0].HoraInicio.Minute, profesionales[0].HoraInicio.Second).AddDays(5).Add(profesionales[0].Prestacion.Duracion),
                    PacienteId = pacientes[0].Id,
                    ProfesionalId = profesionales[0].Id,
                });

                turnos.Add(new Turno
                {
                    Fecha = new DateTime(now.Year, now.Month, now.Day, profesionales[1].HoraInicio.Hour, profesionales[1].HoraInicio.Minute, profesionales[1].HoraInicio.Second).AddDays(1).Add(profesionales[1].Prestacion.Duracion),
                    PacienteId = pacientes[1].Id,
                    ProfesionalId = profesionales[1].Id,
                });

                turnos.Add(new Turno
                {
                    Fecha = new DateTime(now.Year, now.Month, now.Day, profesionales[2].HoraInicio.Hour, profesionales[2].HoraInicio.Minute, profesionales[2].HoraInicio.Second).AddDays(2).Add(profesionales[2].Prestacion.Duracion),
                    PacienteId = pacientes[2].Id,
                    ProfesionalId = profesionales[2].Id,
                });

                turnos.Add(new Turno
                {
                    Fecha = new DateTime(now.Year, now.Month, now.Day, profesionales[3].HoraInicio.Hour, profesionales[3].HoraInicio.Minute, profesionales[3].HoraInicio.Second).AddDays(3).Add(profesionales[3].Prestacion.Duracion),
                    PacienteId = pacientes[3].Id,
                    ProfesionalId = profesionales[3].Id,
                });

                turnos.Add(new Turno
                {
                    Fecha = new DateTime(now.Year, now.Month, now.Day, profesionales[3].HoraInicio.Hour, profesionales[3].HoraInicio.Minute, profesionales[3].HoraInicio.Second).AddDays(6).Add(profesionales[3].Prestacion.Duracion),
                    PacienteId = pacientes[3].Id,
                    ProfesionalId = profesionales[3].Id,
                });
                _context.Turnos.AddRange(turnos);
                _context.SaveChanges();
            }
        }

        private void GenerarFormularios()
        {
            if (_context.Formularios.Count() == 0)
            {
                List<Formulario> formularios = new List<Formulario>();
                List<Paciente> pacientes = _context.Pacientes.ToList();

                formularios.Add(new Formulario
                {
                    Email = pacientes[0].Email,
                    Nombre = pacientes[0].Nombre,
                    Apellido = pacientes[0].Apellido,
                    Titulo = "Aviso",
                    Mensaje = "Hola Doctor, que tal? Le adjunto mis estudios.",
                    PacienteId = pacientes[0].Id,
                });

                formularios.Add(new Formulario
                {
                    Email = pacientes[1].Email,
                    Nombre = pacientes[1].Nombre,
                    Apellido = pacientes[1].Apellido,
                    Titulo = "Consulta",
                    Mensaje = "Buenas tardes Doctor, como esta? Para cuando estarían mis estudios?.",
                    PacienteId = pacientes[1].Id,
                });

                formularios.Add(new Formulario
                {
                    Email = pacientes[2].Email,
                    Nombre = pacientes[2].Nombre,
                    Apellido = pacientes[2].Apellido,
                    Titulo = "Consulta",
                    Mensaje = "Buen día Doctor, perdón las molestias. Quería consultarle si hay turnos disponibles para la semana que viene. Saludos.",
                    PacienteId = pacientes[2].Id,
                });

                formularios.Add(new Formulario
                {
                    Email = pacientes[3].Email,
                    Nombre = pacientes[3].Nombre,
                    Apellido = pacientes[3].Apellido,
                    Titulo = "Aviso y Consulta",
                    Mensaje = "Doctor, buenos días. Ya tengo los estudios realizados, como procedemos con la operacion de vesícula? Saludos.",
                    PacienteId = pacientes[3].Id,
                });
                _context.Formularios.AddRange(formularios);
                _context.SaveChanges();
            }
        }
    }
}
