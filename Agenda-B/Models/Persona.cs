using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Agenda_B.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Agenda_B.Models
{
    public class Persona : IdentityUser<int>
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Apellido { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMaxDoc, MinimumLength = Restricciones.StrMinDoc, ErrorMessage = MsjsError.ErrMinMax)]
        [Display(Name = Alias.DNI)]
        public String DNI { get; set; }

        [Required(ErrorMessage =MsjsError.ErrRequired)]
        [DataType(DataType.EmailAddress, ErrorMessage = MsjsError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        public override String Email { 
            get { return base.Email;}
            set { base.Email = value;} }

        public Direccion Direccion { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [MinLength(Restricciones.StrMin4, ErrorMessage = MsjsError.ErrMinimo)]
        public String Telefono { get; set; }

        [Display(Name = Alias.FechaAlta)]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        public String NombreCompleto{ get {
                return Apellido + ", " + Nombre;
            }
        }
    }
}
