using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Agenda_B.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_B.Models
{
    public class Direccion
    {
        [Display(Name = Alias.PersonaId)]
        [Key, ForeignKey("Persona")]
        public int Id { get; set; }
       
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax3, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public string Calle { get; set; }
       
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        //[StringLength(Restricciones.StrMax1, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        [Range(Restricciones.StrMin1, Restricciones.StrMax5, ErrorMessage = MsjsError.ErrMaxMin)]
        public string Numero { get; set; }
       
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax1, MinimumLength = Restricciones.StrMin5, ErrorMessage = MsjsError.ErrMinMax)]
        [Display(Name = Alias.CodigoPostal)]
        public string CodigoPostal { get; set; }
       
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public string Localidad { get; set; }
       
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public Provincia Provincia { get; set; }

        public Persona Persona { get; set; }
    }
}
