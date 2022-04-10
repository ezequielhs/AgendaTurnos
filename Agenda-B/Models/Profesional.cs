using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Agenda_B.Helpers;

namespace Agenda_B.Models
{
    public class Profesional: Persona
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [StringLength(Restricciones.StrMax1, MinimumLength = Restricciones.StrMin1, ErrorMessage = MsjsError.ErrMinMax)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [Display(Name = Alias.PrestacionId)]
        public int PrestacionId { get; set; }

        public Prestacion Prestacion { get; set; }

        [Display(Name = Alias.HoraInicio)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public DateTime HoraInicio { get; set; }

        [Display(Name = Alias.HoraFin)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        public DateTime HoraFin { get; set; }


        public List<DateTime> HorariosDisponiblesPorDia { 
            get {
                List<DateTime> turnos = new List<DateTime>();
                DateTime horario = this.HoraInicio;

                if (this.Prestacion != null)
                {
                    do
                        {
                            turnos.Add(horario);
                            horario = horario.Add(this.Prestacion.Duracion);
                        } while (horario <= this.HoraFin);
                }
                return turnos;
            }
        }

        [Display(Name = Alias.TurnosTomados)]
        public int TurnosMes
        {
            get
            {
                if(this.Turnos != null)
                {
                    return this.Turnos.Where(t => t.Fecha.Month == DateTime.Now.Month && t.Confirmado).Count();
                }
                return 0;
            }
        }

        [Display(Name = Alias.Sueldo)]
        public decimal SueldoMensual
        {
            get
            {
                if(this.Prestacion != null)
                {
                    return this.TurnosMes * this.Prestacion.Precio;
                }
                return 0;
            }
        }

        public List<Turno> Turnos { get; set; }

    }
}
