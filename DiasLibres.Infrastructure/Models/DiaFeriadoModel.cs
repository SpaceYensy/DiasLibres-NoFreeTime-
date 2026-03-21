using System;

namespace DiasLibres.Infrastructure.Models
{
    public class DiaFeriadoModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsRecurrente { get; set; }

        public string FechaFormateada => Fecha.ToString("dd/MM/yyyy");
        public string DiaSemana => Fecha.DayOfWeek.ToString();
    }
}