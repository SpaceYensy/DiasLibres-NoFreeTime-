using System;

namespace DiasLibres.Application.DTOs
{
    public class DiaResponseDto
    {
        public string Mensaje { get; set; } = string.Empty;
        public bool EsLibre { get; set; }
        public DateTime Fecha { get; set; }
        public string DiaSemana { get; set; } = string.Empty;
        public string? Motivo { get; set; }

        public static DiaResponseDto Libre(DateTime fecha, string motivo)
        {
            return new DiaResponseDto
            {
                Mensaje = "Libre",
                EsLibre = true,
                Fecha = fecha,
                DiaSemana = fecha.DayOfWeek.ToString(),
                Motivo = motivo
            };
        }

        public static DiaResponseDto Trabaja(DateTime fecha)
        {
            return new DiaResponseDto
            {
                Mensaje = "Trabaja",
                EsLibre = false,
                Fecha = fecha,
                DiaSemana = fecha.DayOfWeek.ToString(),
                Motivo = "Día laboral normal"
            };
        }
    }
}
