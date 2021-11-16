using Agenda_B.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Models
{
    public class Formulario
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DataType(DataType.EmailAddress, ErrorMessage = MsjsError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        public String Email { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Apellido { get; set; }

        public Boolean Leido { get; set; } = false;

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax3, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Titulo { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax4, MinimumLength = Restricciones.StrMin2, ErrorMessage = MsjsError.ErrMinMax)]
        public String Mensaje { get; set; }

        [Display(Name = Alias.PacienteId)]
        public int? PacienteId { get; set; }

        public Paciente Paciente { get; set; }
        public String NombreCompleto
        {
            get
            {
                return Apellido + ", " + Nombre;
            }
        }
    }
    
}
