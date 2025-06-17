using LibraryTrabajoFinal.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;
using TrabajoFinal.Servicios.Excepciones;



namespace TrabajoFinal.Controllers
{
    [ApiController]
    [Route("api/cartas")]
    public class CartaController : Controller
    {
        private readonly ICartaService _cartaService;

        public CartaController(ICartaService cartaService)
        {
            _cartaService = cartaService;
        }

        [HttpPost("crear")]
        public IActionResult CrearCarta([FromBody] RequestCrearCarta request)
        {
            int cartaId = _cartaService.CrearCarta(request.Nombre, request.Ataque, request.Defensa, request.Ilustracion, request.SeriesIds);
            return CreatedAtAction(nameof(CrearCarta), new { id = cartaId }, new { Message = "Carta creada exitosamente" });
        }
        //[Authorize]
        [HttpGet("listar-cartas")]
        public IActionResult ListarCartas()
        {
            var cartas = _cartaService.ListarCartas();
            return Ok(cartas);
        }


        [HttpPost("details")]
        public IActionResult ObtenerCartaPorId([FromBody] RequestIdList request)
        {
            if (request == null || request.Ids == null || !request.Ids.Any())
            {
                return BadRequest("Se requiere una lista de IDs de cartas.");
            }
            try
            {
            var cartas = _cartaService.ObtenerCartaPorId(request.Ids);
                if (cartas == null || !cartas.Any())
                {
                    return NotFound("No se encontraron cartas para los IDs proporcionados.");
                }
                return Ok(cartas);
            }
            catch (Exception ex)
            {
                // Loguea la excepción para depuración
                Console.WriteLine($"Error en GetCartasByIds: {ex.Message}");
                return StatusCode(500, "Error interno del servidor al obtener detalles de las cartas.");
            }
        }

        [HttpGet("mazos/{jugadorId}")]
        public IActionResult GetMazosByJugadorId(int jugadorId)
        {
            try
            {
                var mazos = _cartaService.ObtenerMazoPorId(jugadorId);
                return Ok(mazos);

            }
            catch
            {
                return BadRequest(new { Message = "Error interno " });
            }
        }

            [HttpPost("crear-mazo")]
        public IActionResult CrearMazo([FromBody] RequestCrearMazo request
            )
        {
            try
            {
                int mazoId = _cartaService.CrearMazo(request.JugadorId, request.Nombre, request.CartasIds);
                List<int> cartas = request.CartasIds;
                return CreatedAtAction(nameof(CrearMazo), new { id = mazoId }, new { Message = "Mazo creado exitosamente" });
            }
            catch (CantidadCartasMazoInvalidaException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (CartaNoEncontradaException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error interno: {ex.Message}" });

            }

        }
        [HttpGet("{mazoId}/Cartas")] // Ruta: GET api/Cartas/{mazoId}/Cartas
        public IActionResult GetCartasDeMazo(int mazoId)
        {
            try
            {
                var cartas = _cartaService.ObtenerCartasDeMazo(mazoId);

                if (cartas == null || !cartas.Any())
                {
                    // Si no se encuentran cartas para el mazo, o el mazo no existe, es un 404
                    return NotFound($"No se encontraron cartas para el mazo con ID {mazoId}.");
                }
                return Ok(cartas);
            }
            catch (Exception ex)
            {
               Console.WriteLine( "Error al obtener cartas para el mazo con ID {MazoId}", mazoId);
                return StatusCode(500, "Error interno del servidor al obtener las cartas del mazo.");
            }
        }
    }
}
