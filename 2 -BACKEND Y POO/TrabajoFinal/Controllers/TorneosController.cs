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

        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPost("{torneoId}/agregar-series")]
        public IActionResult AgregarSeriesATorneo(int torneoId, [FromBody] RequestAgregarSeries request)
        {
            try
            {
                _torneoService.AgregarSeriesATorneoExistente(torneoId, request.SeriesIds);
                return Ok(new { Message = "Series agregadas exitosamente al torneo." });
            }
            catch (TorneoNoEncontradoException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (FaseRegistroCaducadaException ex)
            {
                return BadRequest(new { Message = $"No se pueden agregar series. El torneo ya no está en fase de registro (fase actual: {ex.FaseActual})." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Error interno del servidor." });
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
    [HttpPost("avanzar-fase/{torneoId}")]
    public IActionResult AvanzarFase(int torneoId)
    {
        try
        {
            var torneo = _torneoService.AvanzarFase(torneoId);
            return torneo
                ? Ok(new { Message = "La fase del torneo avanzó correctamente." })
                : BadRequest(new { Message = "No se pudo avanzar la fase del torneo." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador,Organizador")]
    [HttpPost("avanzar-ronda/{torneoId}")]
    public IActionResult AvanzarRonda(int torneoId)
    {
        try
        {
            bool rondaAvanzada = _torneoService.AvanzarRonda(torneoId);
            if (!rondaAvanzada)
            {
                return BadRequest(new { Message = "No se pudo avanzar la ronda. Asegúrate de que todos los juegos han finalizado." });
            }
            return Ok(new { Message = "Ronda avanzada correctamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Error interno al avanzar de ronda: {ex.Message}" });
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
