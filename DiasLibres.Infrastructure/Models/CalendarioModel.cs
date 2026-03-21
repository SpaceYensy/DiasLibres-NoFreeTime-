namespace DiasLibres.Infrastructure.Models
{
    public class CalendarioModel
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public bool SabadosLaborables { get; set; }
        public bool DomingosLaborables { get; set; }
        public int TotalFeriados { get; set; }
    }
}
