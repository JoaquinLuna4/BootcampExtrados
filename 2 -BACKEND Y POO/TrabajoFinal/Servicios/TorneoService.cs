using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Servicios.Excepciones;


namespace TrabajoFinal.Servicios
{
    public class TorneoService : ITorneoService
    {
        private readonly ITorneoDAO _torneoDAO;

        public TorneoService(ITorneoDAO torneoDAO)
        {
            _torneoDAO = torneoDAO;
        }

        public int CrearTorneo(RequestCrearTorneo torneo)
        {
            // Valida que exista nombre
            if (string.IsNullOrWhiteSpace(torneo.Nombre))
                throw new NombreTorneoInvalidoException(torneo.Nombre);

            // Valida fechas 
            if (torneo.FechaInicio >= torneo.FechaFin)
                throw new FechasTorneoInvalidasException(torneo.FechaInicio, torneo.FechaFin);

            // Crea el torneo con fase por defecto en fase registro 
            var nuevoTorneo = new Torneo
            {
                
                Nombre = torneo.Nombre,
                OrganizadorId = torneo.OrganizadorId,
                FechaInicio = torneo.FechaInicio,
                FechaFin = torneo.FechaFin,
                Pais = torneo.Pais,
                Fase = TorneoFase.Registro // Fase por defecto
            };

            return _torneoDAO.CrearTorneo(nuevoTorneo);
        }

        public Torneo? ObtenerTorneoPorId(int id)
        {
            return _torneoDAO.ObtenerTorneoPorId(id);
        }

        public IEnumerable<Torneo> ObtenerTodosLosTorneos()
        {
            return _torneoDAO.ObtenerTodosLosTorneos();
        }
        public string ObtenerFaseTorneoId(int id)
        {
            return _torneoDAO.ObtenerFaseTorneoPorId(id);
        }
        public bool ActualizarTorneo(Torneo torneo)
        {
            return _torneoDAO.ActualizarTorneo(torneo);
        }
        public bool AvanzarFase(int torneoId)
        {
            // Validar existencia del torneo
            if (!_torneoDAO.TorneoExiste(torneoId))
            {
                throw new InvalidOperationException($"No se encontró el torneo con ID {torneoId}.");
            }

            // Obtener fase actual
            var faseActual = _torneoDAO.ObtenerFaseTorneoPorId(torneoId);
            if (faseActual == TorneoFase.Finalizacion)
            {
                throw new InvalidOperationException($"El torneo con ID {torneoId} ya está finalizado.");
            }

            // Determinar nueva fase
            string nuevaFase = faseActual switch
            {
                TorneoFase.Registro => TorneoFase.Torneo,
                TorneoFase.Torneo => TorneoFase.Finalizacion,
                _ => throw new InvalidOperationException("Fase inválida.")
            };

            // Avanzar la fase directamente como string
            bool faseAvanzada = _torneoDAO.AvanzarFase(torneoId, nuevaFase);

            if (!faseAvanzada)
            {
                throw new InvalidOperationException($"No se pudo avanzar la fase del torneo ID {torneoId}.");
            }

            return true;
        }


        public bool EliminarTorneo(int id)
        {
            return _torneoDAO.EliminarTorneo(id);
        }
    }

}
