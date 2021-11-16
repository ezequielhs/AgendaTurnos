using Agenda_B.Helpers;
using Agenda_B.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda_B.ViewModels
{
    public class RegistracionProfesional
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [EmailAddress(ErrorMessage = MsjsError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax1, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public String Matricula { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DataType(DataType.Password, ErrorMessage = MsjsError.ErrNoValido)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DataType(DataType.Password, ErrorMessage = MsjsError.ErrNoValido)]
        [Compare("Password", ErrorMessage = MsjsError.ErrNoIguales)]
        [Display(Name = Alias.Password)]
        public string ConfirmacionPassword { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMaxDoc, MinimumLength = Restricciones.StrMinDoc, ErrorMessage = MsjsError.ErrMinMax)]
        [Display(Name = Alias.DNI)]
        public string DNI { get; set; }


        [Display(Name = Alias.FechaAlta)]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Display(Name = Alias.PrestacionId)]
        public int PrestacionId { get; set; }

        public Prestacion Prestacion { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [MinLength(Restricciones.StrMin4, ErrorMessage = MsjsError.ErrMinimo)]
        public string Telefono { get; set; }

        [Display(Name = Alias.HoraInicio)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public DateTime HoraInicio { get; set; }

        [Display(Name = Alias.HoraFin)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public DateTime HoraFin { get; set; }

        public Direccion Direccion { get; set; }

    }
}
