using Agenda_B.Helpers;
using Agenda_B.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda_B.ViewModels
{
    public class RegistracionViewModel
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [EmailAddress(ErrorMessage = MsjsError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        public string Email { get; set; }

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
        [Display(Name = Alias.ObraSocial)]
        public ObraSocial ObraSocial { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [MinLength(Restricciones.StrMin4, ErrorMessage = MsjsError.ErrMinimo)]
        public string Telefono { get; set; }


        public Direccion Direccion { get; set; }


    }
}