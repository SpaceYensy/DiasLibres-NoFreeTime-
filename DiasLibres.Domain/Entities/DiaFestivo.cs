using System;
using DiasLibres.Domain.Core;

namespace DiasLibres.Domain.Entities
{
    public class DiaFestivo : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Religioso";
        public int Dia { get; set; }
        public int Mes { get; set; }
        public bool EsMovil { get; set; }
    }
}
