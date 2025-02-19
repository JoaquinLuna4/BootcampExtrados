using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using TrabajoFinal.Servicios.Excepciones;


[ApiController]
    [Route("api/torneos")]
    public class TorneosController : ControllerBase
    {
        private readonly ITorneoService _torneoService;

        public TorneosController(ITorneoService torneoService)
        {
            _torneoService = torneoService;
        }

        // solo organizadores y administradores 
        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPost]
        public IActionResult CrearTorneo([FromBody] RequestCrearTorneo torneo)
        {
        try
        {
            int torneoId = _torneoService.CrearTorneo(torneo);
            return Ok(new { Message = "Torneo creado exitosamente", TorneoId = torneoId });
        }
        catch (NombreTorneoInvalidoException ex)
        {
            return BadRequest(new { Message = ex.Message, Nombre = ex.Nombre });
        }
        catch (FechasTorneoInvalidasException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message,
                FechaInicio = ex.FechaInicio,
                FechaFin = ex.FechaFin
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "Error interno del servidor al intentar crear el torneo." });
        }
    }



        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ObtenerTorneoPorId(int id)
        {
            var torneo = _torneoService.ObtenerTorneoPorId(id);
            if (torneo == null) return NotFound(new { Message = "Torneo no encontrado" });
            return Ok(torneo);
        }


        [Authorize]
        [HttpGet]
        public IActionResult ObtenerTodosLosTorneos()
        {
            var torneos = _torneoService.ObtenerTodosLosTorneos();
            return Ok(torneos);
        }

        // solo organizadores y administradores 
        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPut("{id}")]
        public IActionResult ActualizarTorneo(int id, [FromBody] Torneo torneo)
        {
            torneo.Id = id;
            bool actualizado = _torneoService.ActualizarTorneo(torneo);
            if (!actualizado) return NotFound(new { Message = "Torneo no encontrado" });
            return Ok(new { Message = "Torneo actualizado correctamente" });
        }
    // Avanzar fase del torneo
    [Authorize(Roles = "Organizador,Administrador")]
    [HttpPost("avanzar-fase")]
    public IActionResult AvanzarFase([FromBody] RequestAvanzarFase request)
    {
        try
        {
            var resultado = _torneoService.AvanzarFase(request.torneoId);
            return resultado
                ? Ok(new { Message = "La fase del torneo avanzó correctamente." })
                : BadRequest(new { Message = "No se pudo avanzar la fase del torneo." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    // solo administradores
    [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult EliminarTorneo(int id)
        {
            bool eliminado = _torneoService.EliminarTorneo(id);
            if (!eliminado)
                return NotFound(new { Message = "Torneo no encontrado o ya eliminado." });

            return Ok(new { Message = "Torneo marcado como eliminado." });
        }
    }
