using System;
using System.Threading.Tasks;
using DiasLibres.Domain.Entities;

namespace DiasLibres.Domain.Interfaces
{
    public interface ICalendarioRepository
    {
        Task<CalendarioLaboral?> GetByYearAsync(int year);
        Task<CalendarioLaboral> CreateOrUpdateAsync(CalendarioLaboral calendario);
        Task<bool> ExistsYearAsync(int year);
    }
}
