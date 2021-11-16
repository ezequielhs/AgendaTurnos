using Agenda_B.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Models
{
    public class Turno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DataType(DataType.DateTime, ErrorMessage = MsjsError.ErrDataTypeDateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy h:mm}")]
        [Display(Name = Alias.FechaTurno)]
        public DateTime Fecha { get; set; }

        public Boolean Confirmado { get; set; } = false;
        public Boolean Cancelado { get; set; } = false;

        public Boolean Activo { get; set; } = false;

        [Display(Name = Alias.FechaAlta)]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        public Paciente Paciente { get; set; }

        public Profesional Profesional { get; set; }

        [MaxLength(Restricciones.StrMax4, ErrorMessage = MsjsError.ErrMaximo)]
        [Display(Name = Alias.DescripcionCancelacion)]
        public String DescripcionCancelacion { get; set; }
        
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Display(Name = Alias.PacienteId)]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Display(Name = Alias.ProfesionalId)]
        public int ProfesionalId { get; set; }


        public String TurnoCompleto(Persona persona)
        {
            return Fecha.ToString() + ", " + persona.NombreCompleto;
        }

        public bool TurnoCancelable
        {
            get
            {
                return this.Confirmado && 
                    (this.Fecha - DateTime.Now.AddHours(Constantes.HORAS_PERMITIDAS_CANCELAR)).TotalHours > 0;
            }
        }

        
    }
}
