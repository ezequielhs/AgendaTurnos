using Agenda_B.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [EmailAddress(ErrorMessage = MsjsError.ErrNoValido)]
        public string Email { get; set; }

        [Required(ErrorMessage = MsjsError.ErrRequired)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

    }
}
