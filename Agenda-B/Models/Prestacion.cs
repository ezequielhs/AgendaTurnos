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

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = MsjsError.ErrDataTypeDateTime)]
        public TimeSpan Duracion { get; set; } = new TimeSpan(0, 0, 0);
        
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Range(Restricciones.MinValue2, Restricciones.MaxValue1, ErrorMessage = MsjsError.ErrMinMax)]
        public Decimal Precio { get; set; }
        
        public List<Profesional> Profesionales { get; set; }

    }
}
