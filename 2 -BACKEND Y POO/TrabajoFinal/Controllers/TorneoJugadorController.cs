using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;
using LibraryTrabajoFinal.DTOS;
using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using TrabajoFinal.Servicios.Excepciones;


[ApiController]
[Route("api/torneos/jugadores")]
public class TorneoJugadorController : ControllerBase
{
    private readonly ITorneoJugadorService _torneoJugadorService;

    public TorneoJugadorController(ITorneoJugadorService torneoJugadorService)
    {
        _torneoJugadorService = torneoJugadorService;
        

    }

    // Inscribir un jugador en un torneo
    [Authorize(Roles = "Jugador")]
    [HttpPost("inscribir")]
    public IActionResult InscribirJugador([FromBody] RequestInscripcionTorneo request)
    {
        try
        {
            _torneoJugadorService.InscribirJugador(request.TorneoId, request.JugadorId, request.MazoId);
            return Ok(new { Message = "Jugador inscrito correctamente en el torneo." });
        }
        catch (FaseRegistroCaducadaException ex)
        {
            var mensaje = $"El torneo con ID {ex.TorneoId} ya no está en fase de registro. El torneo actualmente esta en la fase de: {ex.FaseActual}";

            return BadRequest(new { Message = "El torneo ya no está en fase de registro" }); 
        }
        catch (JugadorNoPoseeMazoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (MazoNoValidoParaTorneoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (MazoYaAsignadoATorneoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (JugadorYaInscriptoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex) // Para cualquier otra excepción
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            return StatusCode(500, new { Message = "Error interno del servidor", Error = ex.Message });
        }

    }

    [Authorize(Roles = "Jugador")]
    [HttpPost("{torneoId}/registrar-mazo")]
    public IActionResult RegistrarMazoEnTorneo(int torneoId, [FromBody] RequestRegistroMazo request)
    {
        try
        {
            bool registrado = _torneoJugadorService.InscribirJugador(torneoId, request.JugadorId, request.MazoId);

            if (!registrado)
            {
                return BadRequest(new { Message = "No se pudo registrar el mazo en el torneo." });
            }

            return Ok(new { Message = "Mazo registrado correctamente en el torneo." });
        }
        catch (FaseRegistroCaducadaException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (JugadorYaTieneMazoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (MazoNoValidoParaTorneoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (JugadorYaInscriptoException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "Error interno del servidor" });
        }
    }


    // Obtener lista de jugadores inscritos en un torneo
    [Authorize(Roles = "Administrador,Organizador")]
    [HttpGet("{torneoId}")]
    public IActionResult ObtenerJugadoresPorTorneo(int torneoId)
    {
        var jugadores = _torneoJugadorService.ObtenerJugadoresPorTorneo(torneoId);
        return Ok(jugadores);
    }

    // Desinscribir un jugador antes del inicio del torneo
    [Authorize(Roles = "Jugador")]
    [HttpDelete("desinscribir")]
    public IActionResult DesinscribirJugador([FromBody] RequestDesinscripcionTorneo request)
    {
        try
        {
            _torneoJugadorService.DesinscribirJugador(request.TorneoId, request.JugadorId);
            return Ok(new { Message = "Jugador desinscripto correctamente." });
        }
        catch (FaseRegistroCaducadaException ex)
        {
            var mensaje = $"El jugador no se puede desinscribir del torneo con ID {ex.TorneoId} porque ya no esta en fase de registro. La fase actual es: {ex.FaseActual}";
            
            return BadRequest(new { Message = "No se puede desinscribir al jugador en este momento." }); 


        }

    }
}
