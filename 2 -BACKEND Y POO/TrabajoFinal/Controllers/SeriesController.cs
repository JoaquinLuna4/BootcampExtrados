using LibraryTrabajoFinal.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;

namespace TrabajoFinal.Controllers
{
    [ApiController]
    [Route("api/series")]
    public class SeriesController : ControllerBase
    {
        private readonly ISerieService _serieService;

        public SeriesController(ISerieService serieService)
        {
            _serieService = serieService;
        }

        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPost]
        public IActionResult CrearSerie([FromBody] Serie serie)
        {
            try
            {
                int serieId = _serieService.CrearSerie(serie.Nombre, serie.FechaLanzamiento);
                return CreatedAtAction(nameof(ObtenerSeriePorId), new { id = serieId }, new { Message = "Serie creada exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerSeriePorId(int id)
        {
            var serie = _serieService.ObtenerSeriePorId(id);
            if (serie == null)
                return NotFound(new { Message = "Serie no encontrada." });

            return Ok(serie);
        }

        [HttpGet]
        public IActionResult ObtenerTodasLasSeries()
        {
            var series = _serieService.ObtenerTodasLasSeries();
            return Ok(series);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult EliminarSerie(int id)
        {
            bool eliminada = _serieService.EliminarSerie(id);
            if (!eliminada)
                return NotFound(new { Message = "Serie no encontrada o no pudo ser eliminada." });

            return NoContent();
        }
    }
}
