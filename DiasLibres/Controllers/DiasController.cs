using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DiasLibres.Application.Services;
using DiasLibres.Application.DTOs;

namespace DiasLibres.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DiasController : ControllerBase
    {
        private readonly IDiaLaboralService _diaService;
        private readonly IDiaFeriadoRepository _feriadoRepository;
        private readonly ILogger<DiasController> _logger;

        public DiasController(
            IDiaLaboralService diaService,
            IDiaFeriadoRepository feriadoRepository,
            ILogger<DiasController> logger)
        {
            _diaService = diaService;
            _feriadoRepository = feriadoRepository;
            _logger = logger;
        }


        [HttpGet("hoy")]
        [ProducesResponseType(typeof(DiaResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<DiaResponseDto>> VerificarHoy()
        {
            var resultado = await _diaService.VerificarHoyAsync();
            return Ok(resultado);
        }

        [HttpGet("verificar")]
        [ProducesResponseType(typeof(DiaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DiaResponseDto>> VerificarFecha([FromQuery] string? fecha = null)
        {
            try
            {
                DateTime fechaVerificar;

                if (string.IsNullOrEmpty(fecha))
                {
                    fechaVerificar = DateTime.Today;
                }
                else
                {
                    if (!DateTime.TryParse(fecha, out fechaVerificar))
                    {
                        return BadRequest(new { error = "Formato de fecha inválido. Use yyyy-MM-dd" });
                    }
                }

                var resultado = await _diaService.VerificarDiaAsync(fechaVerificar);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar fecha {fecha}", fecha);
                return StatusCode(500, new { error = "Error interno del servidor" });
            }
        }

        [HttpGet("simple")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> VerificarSimple([FromQuery] string? fecha = null)
        {
            try
            {
                DateTime fechaVerificar = string.IsNullOrEmpty(fecha)
                    ? DateTime.Today
                    : DateTime.Parse(fecha);

                var resultado = await _diaService.VerificarDiaAsync(fechaVerificar);
                return Ok(resultado.Mensaje);
            }
            catch
            {
                return BadRequest("Formato de fecha inválido");
            }
        }

        [HttpGet("feriados")]
        [ProducesResponseType(typeof(IEnumerable<DiaFeriado>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DiaFeriado>>> GetFeriados([FromQuery] int? year = null)
        {
            if (year.HasValue)
            {
                var feriados = await _feriadoRepository.GetByYearAsync(year.Value);
                return Ok(feriados);
            }

            var todos = await _feriadoRepository.GetAllAsync();
            return Ok(todos);
        }

        [HttpPost("feriados")]
        [ProducesResponseType(typeof(DiaFeriado), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DiaFeriado>> AgregarFeriado([FromBody] CrearFeriadoDto dto)
        {
            try
            {
                var feriado = new DiaFeriado
                {
                    Fecha = dto.Fecha,
                    Nombre = dto.Nombre,
                    Tipo = dto.Tipo,
                    Descripcion = dto.Descripcion,
                    EsRecurrente = dto.EsRecurrente
                };

                var nuevo = await _feriadoRepository.AddAsync(feriado);
                return CreatedAtAction(nameof(GetFeriados), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                Status = "API funcionando",
                Version = "1.0.0",
                Timestamp = DateTime.UtcNow,
                Endpoints = new[]
                {
                    "GET /api/dias/hoy",
                    "GET /api/dias/verificar?fecha=YYYY-MM-DD",
                    "GET /api/dias/simple?fecha=YYYY-MM-DD",
                    "GET /api/dias/feriados",
                    "POST /api/dias/feriados"
                }
            });
        }
    }

    public class CrearFeriadoDto
    {
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Nacional";
        public string? Descripcion { get; set; }
        public bool EsRecurrente { get; set; } = true;
    }
}
