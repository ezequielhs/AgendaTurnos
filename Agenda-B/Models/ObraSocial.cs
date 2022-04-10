using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Agenda_B.Models
{
    public enum ObraSocial
    {
        [Display(Name = "Galeno")]
        GALENO,
        [Display(Name = "Hope")]
        HOPE,
        [Display(Name = "IOMA")]
        IOMA,
        [Display(Name = "Luis Pasteur")]
        LUIS_PASTEUR,
        [Display(Name = "Medicus")]
        MEDICUS,
        [Display(Name = "OSDE")]
        OSDE,
        [Display(Name = "OSECAC")]
        OSECAC,
        [Display(Name = "PAMI")]
        PAMI,
        [Display(Name = "Sancor Salud")]
        SANCOR_SALUD,
        [Display(Name = "Swiss Medical")]
        SWISS_MEDICAL,
        [Display(Name = "No Posee")]
        NO_POSEE
    }
}
