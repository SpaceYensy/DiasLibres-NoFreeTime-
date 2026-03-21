using DiasLibres.Domain.Core;
using DiasLibres.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DiasLibres.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DiaFeriado> DiasFeriados { get; set; }
        public DbSet<DiaFestivo> DiasFestivos { get; set; }
        public DbSet<CalendarioLaboral> CalendariosLaborales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiaFeriado>(entity =>
            {
                entity.HasIndex(e => e.Fecha).IsUnique();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.HasData(
                    new DiaFeriado { Id = 1, Fecha = new DateTime(2024, 1, 1), Nombre = "Año Nuevo", Tipo = "Nacional", EsRecurrente = true },
                    new DiaFeriado { Id = 2, Fecha = new DateTime(2024, 12, 25), Nombre = "Navidad", Tipo = "Nacional", EsRecurrente = true },
                    new DiaFeriado { Id = 3, Fecha = new DateTime(2024, 5, 1), Nombre = "Día del Trabajo", Tipo = "Nacional", EsRecurrente = true },
                    new DiaFeriado { Id = 4, Fecha = new DateTime(2024, 7, 9), Nombre = "Día de la Independencia", Tipo = "Nacional", EsRecurrente = true },
                    new DiaFeriado { Id = 5, Fecha = new DateTime(2024, 12, 8), Nombre = "Inmaculada Concepción", Tipo = "Religioso", EsRecurrente = true }
                );
            });

            modelBuilder.Entity<CalendarioLaboral>(entity =>
            {
                entity.HasIndex(e => e.Anio).IsUnique();
                entity.Property(e => e.SabadosLaborables).HasDefaultValue(false);
                entity.Property(e => e.DomingosLaborables).HasDefaultValue(false);
                entity.HasData(
                    new CalendarioLaboral { Id = 1, Anio = 2024, SabadosLaborables = false, DomingosLaborables = false }
                );
            });
        }
    }
}
