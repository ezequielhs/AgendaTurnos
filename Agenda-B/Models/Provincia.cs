using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Agenda_B.Helpers;

namespace Agenda_B.Models
{
    public enum Provincia
    {
        [Display(Name = "Buenos Aires")]
        BUENOS_AIRES,
        [Display(Name = "Capital Federal")]
        CAPITAL_FEDERAL,
        [Display(Name = "Catamarca")]
        CATAMARCA,
        [Display(Name = "Chacho")]
        CHACO,
        [Display(Name = "Chubut")]
        CHUBUT,
        [Display(Name = "Córdoba")]
        CORDOBA,
        [Display(Name = "Corrientes")]
        CORRIENTES,
        [Display(Name = "Entre Ríos")]
        ENTRE_RIOS,
        [Display(Name = "Formosa")]
        FORMOSA,
        [Display(Name = "Jujuy")]
        JUJUY,
        [Display(Name = "La Pampa")]
        LA_PAMPA,
        [Display(Name = "La Rioja")]
        LA_RIOJA,
        [Display(Name = "Mendoza")]
        MENDOZA,
        [Display(Name = "Misiones")]
        MISIONES,
        [Display(Name = "Neuquén")]
        NEUQUEN,
        [Display(Name = "Río Negro")]
        RIO_NEGRO,
        [Display(Name = "San Juan")]
        SAN_JUAN,
        [Display(Name = "San Luis")]
        SAN_LUIS,
        [Display(Name = "Santa Cruz")]
        SANTA_CRUZ,
        [Display(Name = "Santa Fe")]
        SANTA_FE,
        [Display(Name = "Santiago del Estero")]
        SANTIAGO_DEL_ESTERO,
        [Display(Name = "Salta")]
        SALTA,
        [Display(Name = "Tierra del Fuego")]
        TIERRA_DEL_FUEGO,
        [Display(Name = "Tucumán")]
        TUCUMAN
    }
}
