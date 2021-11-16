using Agenda_B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B.Data
{
    public class AgendaContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public AgendaContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser<int>>().ToTable("Personas");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("RolesPersonas");
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Formulario> Formularios { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Prestacion> Prestaciones { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
    }
}
