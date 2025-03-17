using LibraryTrabajoFinal.DTOS;
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
    }
}
