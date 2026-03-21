using System;
using System.Collections.Generic;
using DiasLibres.Domain.Core;

namespace DiasLibres.Domain.Entities
{
    public class CalendarioLaboral : BaseEntity
    {
        public int Anio { get; set; }
        public bool SabadosLaborables { get; set; } = false;
        public bool DomingosLaborables { get; set; } = false;

        public ICollection<DiaFeriado> Feriados { get; set; } = new List<DiaFeriado>();
        public ICollection<DiaFestivo> Festivos { get; set; } = new List<DiaFestivo>();

        public bool EsDiaLibre(DateTime fecha)
        {
            if (!SabadosLaborables && fecha.DayOfWeek == DayOfWeek.Saturday)
                return true;
            if (!DomingosLaborables && fecha.DayOfWeek == DayOfWeek.Sunday)
                return true;

            foreach (var feriado in Feriados)
            {
                if (feriado.EsRecurrente && feriado.Fecha.Month == fecha.Month && feriado.Fecha.Day == fecha.Day)
                    return true;
                if (!feriado.EsRecurrente && feriado.Fecha.Date == fecha.Date)
                    return true;
            }

            foreach (var festivo in Festivos)
            {
                if (festivo.Dia == fecha.Day && festivo.Mes == fecha.Month)
                    return true;
            }

            return false;
        }
    }
}