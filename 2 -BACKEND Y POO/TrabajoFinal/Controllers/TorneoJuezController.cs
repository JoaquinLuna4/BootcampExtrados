using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;
using LibraryTrabajoFinal.EntidadesRelaciones;

namespace TrabajoFinal.Controllers
{
    [ApiController]
    [Route("api/torneos/jueces")]
    public class TorneoJuezController : ControllerBase
    {
        private readonly ITorneoJuezService _torneoJuezService;

        public TorneoJuezController(ITorneoJuezService torneoJuezService)
        {
            _torneoJuezService = torneoJuezService;
        }

        // Asignar juez a un torneo
        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPost("asignar")]
        public IActionResult AsignarJuezATorneo([FromBody] TorneoJuez request)
        {
            try
            {
                bool resultado = _torneoJuezService.AsignarJuezATorneo(request.TorneoId, request.JuezId);
                if (!resultado) return BadRequest(new { Message = "No se pudo asignar el juez al torneo." });

                return Ok(new { Message = "Juez asignado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Eliminar juez de un torneo
        [Authorize(Roles = "Administrador,Organizador")]
        [HttpDelete("eliminar")]
        public IActionResult EliminarJuezDeTorneo([FromBody] TorneoJuez request)
        {
            bool resultado = _torneoJuezService.EliminarJuezDeTorneo(request.TorneoId, request.JuezId);
            if (!resultado) return BadRequest(new { Message = "No se pudo eliminar el juez del torneo." });

            return Ok(new { Message = "Juez eliminado correctamente." });
        }

        // Obtener jueces de un torneo
        [Authorize]
        [HttpGet("{torneoId}")]
        public IActionResult ObtenerJuecesPorTorneo(int torneoId)
        {
            var jueces = _torneoJuezService.ObtenerJuecesPorTorneo(torneoId);
            return Ok(jueces);
        }
    }
}
