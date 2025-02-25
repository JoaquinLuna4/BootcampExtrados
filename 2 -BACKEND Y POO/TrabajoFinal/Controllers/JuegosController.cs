﻿using LibraryTrabajoFinal.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios;

namespace TrabajoFinal.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/juegos")]
    public class JuegosController : ControllerBase
    {
        private readonly IJuegoService _juegoService;

        public JuegosController(IJuegoService juegoService)
        {
            _juegoService = juegoService;
        }

        [Authorize(Roles = "Organizador")]
        [HttpPost("crear")]
        public IActionResult CrearJuego([FromBody] RequestCrearJuego request)
        {
            var juegoId = _juegoService.CrearJuego(request.TorneoId, request.Jugador1Id, request.Jugador2Id, request.FechaHoraInicio);
            return Ok(new { Message = "Juego creado correctamente", JuegoId = juegoId });
        }

        [Authorize(Roles = "Administrador,Organizador,Juez")]
        [HttpGet("/api/torneos/{torneoId}/juegos")]
        public IActionResult ObtenerJuegosPorTorneo(int torneoId)
        {
            var juegos = _juegoService.ObtenerJuegosPorTorneo(torneoId);

            if (juegos == null || !juegos.Any())
            {
                return NotFound(new { Message = "No hay juegos registrados para este torneo." });
            }

            return Ok(juegos);
        }


        [Authorize(Roles = "Juez")]
        [HttpPost("finalizar")]
        public IActionResult FinalizarJuego([FromBody] RequestFinalizarJuego request)
        {
            _juegoService.FinalizarJuego(request.Id, request.GanadorId, request.FechaHoraFin);
            return Ok(new { Message = "Juego finalizado correctamente" });
        }

    }
}
