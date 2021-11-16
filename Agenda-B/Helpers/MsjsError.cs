using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Helpers
{
    public static class MsjsError
    {
        public const string ErrRequired = "El campo {0} es requerido";
        public const string ErrMaximo = "La longitud maxima de este campo es de {1} caracteres";
        public const string ErrMinimo = "La longitud minima de este campo es de {1} caracteres";
        public const string ErrDataTypeDateTime = "Por favor ingresar una fecha valida en el campo {0}";
        public const string ErrNoValido = "El campo {0} no es válido";
        public const string ErrMinMax = "El campo {0} necesita un minimo de {2} caracteres y un maximo de {1}";
        public const string ErrMaxMin = "El campo {0} necesita un minimo de {1} caracteres y un maximo de {2}";
        public const string ErrNoIguales = "Las contraseñas no son iguales.";
        
    }
}
