using System;
using System.Threading.Tasks;
using DiasLibres.Application.DTOs;
using DiasLibres.Domain.Interfaces;

namespace DiasLibres.Application.Services
{
    public interface IDiaLaboralService
    {
        Task<DiaResponseDto> VerificarDiaAsync(DateTime fecha);
        Task<DiaResponseDto> VerificarHoyAsync();
    }

    public class DiaLaboralService : IDiaLaboralService
    {
        private readonly IDiaFeriadoRepository _feriadoRepository;
        private readonly ICalendarioRepository _calendarioRepository;

        public DiaLaboralService(
            IDiaFeriadoRepository feriadoRepository,
            ICalendarioRepository calendarioRepository)
        {
            _feriadoRepository = feriadoRepository;
            _calendarioRepository = calendarioRepository;
        }

        public async Task<DiaResponseDto> VerificarDiaAsync(DateTime fecha)
        {
            var fechaDate = fecha.Date;
            var calendario = await _calendarioRepository.GetByYearAsync(fechaDate.Year);

            if (fechaDate.DayOfWeek == DayOfWeek.Saturday &&
                (calendario == null || !calendario.SabadosLaborables))
            {
                return DiaResponseDto.Libre(fechaDate, "Sábado - Fin de semana");
            }

            if (fechaDate.DayOfWeek == DayOfWeek.Sunday &&
                (calendario == null || !calendario.DomingosLaborables))
            {
                return DiaResponseDto.Libre(fechaDate, "Domingo - Fin de semana");
            }

            var feriado = await _feriadoRepository.GetByDateAsync(fechaDate);
            if (feriado != null)
            {
                return DiaResponseDto.Libre(fechaDate, feriado.Nombre);
            }

            return DiaResponseDto.Trabaja(fechaDate);
        }

        public async Task<DiaResponseDto> VerificarHoyAsync()
        {
            return await VerificarDiaAsync(DateTime.Today);
        }
    }
}