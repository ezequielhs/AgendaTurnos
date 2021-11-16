using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Agenda_B.Helpers;

namespace Agenda_B.Models
{
    public class Paciente: Persona
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Display(Name = Alias.ObraSocial)]
        public ObraSocial ObraSocial { get; set; }
        
        public List<Turno> Turnos { get; set; }

        public List<Formulario> Formularios { get; set; }
    }
}
