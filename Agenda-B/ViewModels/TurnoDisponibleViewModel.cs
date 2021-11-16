using Agenda_B.Models;
using System;
using System.Collections.Generic;

namespace Agenda_B.ViewModels
{
    public class TurnoDisponibleViewModel
    {
        public List<DateTime> FechasDisponibles { get; set; }
        public DateTime FechaTurno { get; set; }
        public Profesional Profesional { get; set; }
        public int ProfesionalId { get; set; }
        public int PacienteId { get; set; }

        public Prestacion Prestacion { get; set; }

    }
}
