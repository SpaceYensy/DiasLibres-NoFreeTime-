using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiasLibres.Domain.Entities;

namespace DiasLibres.Domain.Interfaces
{
    public interface IDiaFeriadoRepository
    {
        Task<DiaFeriado?> GetByIdAsync(int id);
        Task<IEnumerable<DiaFeriado>> GetAllAsync();
        Task<IEnumerable<DiaFeriado>> GetByYearAsync(int year);
        Task<DiaFeriado?> GetByDateAsync(DateTime fecha);
        Task<DiaFeriado> AddAsync(DiaFeriado feriado);
        Task UpdateAsync(DiaFeriado feriado);
        Task DeleteAsync(int id);
        Task<bool> ExistsByDateAsync(DateTime fecha);
    }
}
