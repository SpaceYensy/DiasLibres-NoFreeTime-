using System;
using DiasLibres.Domain.Core;

namespace DiasLibres.Domain.Entities
{
    public class DiaFeriado : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Nacional";
        public string? Descripcion { get; set; }
        public bool EsRecurrente { get; set; } = true;

        public int? AnioEspecifico { get; set; } 
    }
}
