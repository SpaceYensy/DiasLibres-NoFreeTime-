using System;
using System.Collections.Generic;
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
    public class DiaFeriadoRepository : BaseRepository<DiaFeriado>, IDiaFeriadoRepository
    {
        public DiaFeriadoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DiaFeriado>> GetByYearAsync(int year)
        {
            return await _dbSet
                .Where(f => f.Fecha.Year == year || (f.EsRecurrente && f.Fecha.Year == DateTime.Now.Year))
                .OrderBy(f => f.Fecha)
                .ToListAsync();
        }

        public async Task<DiaFeriado?> GetByDateAsync(DateTime fecha)
        {
            return await _dbSet
                .FirstOrDefaultAsync(f => f.Fecha.Date == fecha.Date ||
                    (f.EsRecurrente && f.Fecha.Month == fecha.Month && f.Fecha.Day == fecha.Day));
        }

        public async Task<bool> ExistsByDateAsync(DateTime fecha)
        {
            return await _dbSet
                .AnyAsync(f => f.Fecha.Date == fecha.Date);
        }

        public override async Task<DiaFeriado> AddAsync(DiaFeriado entity)
        {
            if (await ExistsByDateAsync(entity.Fecha))
            {
                throw new FeriadoDuplicadoException(entity.Fecha);
            }
            return await base.AddAsync(entity);
        }
    }
}