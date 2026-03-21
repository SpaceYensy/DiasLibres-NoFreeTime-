using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiasLibres.Domain.Entities;
using DiasLibres.Domain.Interfaces;
using DiasLibres.Infrastructure.Context;
using DiasLibres.Infrastructure.Core;
using DiasLibres.Infrastructure.Core.Exceptions;

namespace DiasLibres.Infrastructure.Repositories
{
    public class CalendarioRepository : BaseRepository<CalendarioLaboral>, ICalendarioRepository
    {
        public CalendarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CalendarioLaboral?> GetByYearAsync(int year)
        {
            return await _dbSet
                .Include(c => c.Feriados)
                .Include(c => c.Festivos)
                .FirstOrDefaultAsync(c => c.Anio == year);
        }

        public async Task<CalendarioLaboral> CreateOrUpdateAsync(CalendarioLaboral calendario)
        {
            var existing = await GetByYearAsync(calendario.Anio);

            if (existing != null)
            {
                existing.SabadosLaborables = calendario.SabadosLaborables;
                existing.DomingosLaborables = calendario.DomingosLaborables;
                await UpdateAsync(existing);
                return existing;
            }

            return await AddAsync(calendario);
        }

        public async Task<bool> ExistsYearAsync(int year)
        {
            return await _dbSet.AnyAsync(c => c.Anio == year);
        }
    }
}
