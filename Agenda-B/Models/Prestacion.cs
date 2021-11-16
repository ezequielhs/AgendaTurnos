using Agenda_B.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Models
{
    public class Prestacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax2, MinimumLength = Restricciones.StrMin2, ErrorMessage = MsjsError.ErrMinMax)]
        public String Nombre { get; set; }
        
        [MaxLength(Restricciones.StrMax3, ErrorMessage = MsjsError.ErrMaximo)]
        public String Descripcion { get; set; }
        
        [DataType(DataType.Time, ErrorMessage = MsjsError.ErrNoValido)]
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public TimeSpan Duracion { get; set; }
        
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Range(Restricciones.MinValue2, Restricciones.MaxValue1, ErrorMessage = MsjsError.ErrMinMax)]
        public Decimal Precio { get; set; }
        
        public List<Profesional> Profesionales { get; set; }

    }
}
